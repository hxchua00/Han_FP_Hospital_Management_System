using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using HospitalManagementWebApi.DBContext;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Worker")]
    public class WorkerController : ApiController, IWorkerController
    { 
        private IConfigurationController _config;
        private HospitalManagementDBContext HMdBContext;
        public WorkerController()
        {
            HMdBContext = new HospitalManagementDBContext();
            _config = new ConfigurationController(); ;
        }

        [HttpPost]
        [Route("AddPatient")]
        //Add new patient to database
        public IHttpActionResult AddPatient([FromBody]PatientDTO patientdto)
        {
            if(patientdto != null)
            {
                HMdBContext.PatientList.Add(MapToModel(patientdto));
                HMdBContext.SaveChanges();
                return Ok($"Successfully added {patientdto.PatientName}, Patient ID: {patientdto.PatientID}");
            }
            else
            {
                return BadRequest("Error adding patient information");
            }
        }

        [HttpPost]
        [Route("AdmitPatient")]
        //Admit the patient to the respective department
        //Inclusive of consultation of doctors.
        public IHttpActionResult AdmitPatient(int ID, [FromBody]PatientVisitRecordDTO visitRecord)
        {
            Patient patient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();
            if(visitRecord != null)
            {
                HMdBContext.VisitRecords.Add(MapToModel(visitRecord));
                //patient.VisitHistory.Add(MapToModel(visitRecord));

                HMdBContext.SaveChanges();

                return Ok($"Visit record for {patient.PatientName} succesfully added");
            }
            else
            {
                return BadRequest("Error adding visit record information");
            }
        }

        [HttpGet]
        [Route("ViewPatientInfo")]
        //Search through database for Patient's Information via Reference ID given to them
        public IHttpActionResult ViewPatientInfo(int ID)
        {
            Patient patient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();
            if (patient != null)
            {
                return Ok(MapToDTO(patient));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("GenerateBill")]
        public BillDTO GenerateBill(int ID)
        {
            Patient TargetPatient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();
            double DeptPrice = GetDepartmentPrice(ID);
            double WardPrice = (GetWardPrice(ID));
            double MedPrice = GetMedicinePrice(ID);
            double TotalBill = DeptPrice + WardPrice + MedPrice;
            double GST = TotalBill * _config.Gst;
            
            double FinalBill = TotalBill + GST;
            
            foreach (PatientVisitRecord history in TargetPatient.VisitHistory)
            {
                if (history.BillInformation != null && 
                    history.BillInformation.Status == BillStatusEnum.Paid)
                    continue;
                else if(history.BillInformation==null)
                {
                    BillDTO bill = new BillDTO(_config.TotalBillCounter + 1, GST,FinalBill, BillStatusEnum.Unpaid);
                    bill.BillID = _config.TotalBillCounter + 1;
                    bill.GST = GST;
                    bill.TotalAmount = FinalBill;
                    bill.Status = BillStatusEnum.Unpaid;
                    HMdBContext.SaveChanges();
                    return bill;
                }
            }
            throw new Exception("Unable to generate bill");
        }

        [HttpPatch]
        [Route("SettleBill")]
        public IHttpActionResult SettleBill(int ID, int BillID)
        {
            Bill BillPayment = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.Where(x=>x.BillInformation.BillID==BillID).FirstOrDefault().BillInformation;
            if(BillPayment!=null)
            {
                BillPayment.Status = BillStatusEnum.Paid;
                return Ok("Bill Paid Successfully");
            }
            return BadRequest("No Bills to pay!!!");
        }

        [HttpGet]
        [Route("GetPatientName")]
        public string GetPatientName(int ID)
        {
            Patient TargetPatient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();

            if (TargetPatient != null)
            {
                string pName = TargetPatient.PatientName;

                return pName;
            }
            return ("Patient not found");

        }

        [HttpGet]
        [Route("GetPatientDept")]
        //Returns Department the patient is admitted to
        public DepartmentEnum GetPatientDept(int ID)
        {
            Patient TargetPatient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();

            if (TargetPatient != null)
            {
                DepartmentEnum pDept = TargetPatient.VisitHistory.FirstOrDefault().Department;
                return pDept;
            }
            throw new Exception("Patient not found");
        }

        [HttpGet]
        [Route("GetPatientWard")]
        //Returns Ward the patient is admitted to
        public WardEnum GetPatientWard(int ID)
        {
            Patient TargetPatient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();

            if (TargetPatient != null)
            {
                WardEnum pWard = TargetPatient.VisitHistory.FirstOrDefault().Ward;

                return pWard;
            }
            throw new Exception("Patient not found");
        }
        [HttpGet]
        [Route("GetPatientMeds")]
        //Returns list of medicines from VisitHistory
        public List<string> GetPatientMedicineList(int ID)
        {
            Patient TargetPatient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();

            if (TargetPatient != null)
            {
                List<string> pMeds = TargetPatient.VisitHistory.FirstOrDefault().ListOfMedicines;

                return pMeds;
            }
            throw new Exception("Patient not found");
        }

        [HttpGet]
        [Route("GetPatientVisitRecordsCount")]
        public int GetPatientVisitRecordsCount(int ID)
        {
            Patient TargetPatient = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();

            if (TargetPatient != null)
            {
                int visitHistoryCount = TargetPatient.VisitHistory.Count;

                return visitHistoryCount;
            }
            throw new Exception("Patient not found");
        }

        [HttpGet]
        [Route("GetDepartmentPrice")]
        //Calculate total amount to be paid by patient
        public double GetDepartmentPrice(int ID)
        {
            double price = 0;
            if (GetPatientDept(ID) == DepartmentEnum.Outpatient)
            {
                price = 30.00;
            }
            else if (GetPatientDept(ID) == DepartmentEnum.Inpatient)
            {
                price = 100.00;
            }
            else if (GetPatientDept(ID) == DepartmentEnum.Emergency)
            {
                price = 250.00;
            }

            return price;
        }

        [HttpGet]
        [Route("GetWardPrice")]
        public double GetWardPrice(int ID)
        {
            double price = 0;
            if (GetPatientWard(ID) == WardEnum.ClassA)
            {
                price = 250.00;
            }
            else if (GetPatientWard(ID) == WardEnum.ClassB)
            {
                price = 150.00;
            }
            else if (GetPatientWard(ID) == WardEnum.ClassC)
            {
                price = 100.00;
            }
            else if (GetPatientWard(ID) == WardEnum.Private)
            {
                price = 500.00;
            }

            return price;
        }

        [HttpGet]
        [Route("GetMedicinePrice")]
        public double GetMedicinePrice(int ID)
        {
            double price = 0;
            for (int i = 0; i < GetPatientMedicineList(ID).Count; i++)
            {
                if (GetPatientMedicineList(ID)[i] == "Paracetamol")
                {
                    price += 8.85;
                }
                else if (GetPatientMedicineList(ID)[i] == "Antibiotics")
                {
                    price += 15.40;
                }
                else if (GetPatientMedicineList(ID)[i] == "AntiInflammation")
                {
                    price += 10.75;
                }
                else if (GetPatientMedicineList(ID)[i] == "Antihistamine")
                {
                    price += 20.90;
                }
                else if (GetPatientMedicineList(ID)[i] == "Lozenges")
                {
                    price += 6.35;
                }
                else if (GetPatientMedicineList(ID)[i] == "Painkillers")
                {
                    price += 8.50;
                }
            }

            return price;
        }

        public bool ValidatePatient(int ID)
        {
            Patient pObj = HMdBContext.PatientList.Where(x => x.PatientID == ID).FirstOrDefault();
            if (pObj != null)
                return true;
            else
                return false;
        }

        private PatientDTO MapToDTO(Patient patient)
        {
            List<PatientVisitRecordDTO> recorddtoList = new List<PatientVisitRecordDTO>();
            foreach (PatientVisitRecord item in patient.VisitHistory)
                recorddtoList.Add(MapToDTO(item));
            PatientDTO dtoObj = new PatientDTO(patient.PatientID, patient.PatientName, patient.Ethnicity, patient.PatientHeight, patient.PatientWeight, patient.PatientGender, patient
                .Address, patient.ContactNum, patient.PatientAge);
            dtoObj.AddPatientVisitInformation(recorddtoList);
            return dtoObj;
        }
        private Patient MapToModel(PatientDTO patient)
        {
            List<PatientVisitRecord> recorddtoList = new List<PatientVisitRecord>();
            foreach (PatientVisitRecordDTO item in patient.VisitHistory)
                recorddtoList.Add(MapToModel(item));
            Patient modelObj = new Patient(patient.PatientID, patient.PatientName, patient.Ethnicity, patient.PatientHeight, patient.PatientWeight, patient.PatientGender, patient
                .Address, patient.ContactNum, patient.PatientAge);
            modelObj.AddPatientVisitInformation(recorddtoList);
            return modelObj;
        }

        private PatientVisitRecordDTO MapToDTO(PatientVisitRecord record)
        {
            BillDTO bidto = MapToDTO(record.BillInformation);          
            return new PatientVisitRecordDTO(record.RecordNumber, record.DoctorInCharge,record.Department,record.Ward,record.ListOfSymptoms,record.ListOfMedicines, bidto, record.PatientID);
        }
        private PatientVisitRecord MapToModel(PatientVisitRecordDTO record)
        {
            Bill bidto = MapToModel(record.BillInformation);
            return new PatientVisitRecord(record.RecordNumber, record.DoctorInCharge, record.Department, record.Ward, record.ListOfSymptoms, record.ListOfMedicines, bidto, record.PatientID);
        }

        private BillDTO MapToDTO(Bill bill)
        {
            return new BillDTO(bill.BillID, bill.GST, bill.TotalAmount, bill.Status);
        }
        private Bill MapToModel(BillDTO bill)
        {
            return new Bill(bill.BillID, bill.GST, bill.TotalAmount, bill.Status);
        }
    }
}
