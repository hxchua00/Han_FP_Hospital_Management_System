using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using HospitalManagementWebApi.Models;
using HospitalManagementWebApi.Interfaces;
using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Worker")]
    public class WorkerController : ApiController, IHospitalController
    {
        List<Patient> AllPatientInfo = new List<Patient>();            //Stores all pateint information
        IConfigurationController _config;
        public WorkerController()
        {
            _config = new ConfigurationController(); ;
            Initialize();
        }
        
        public void Initialize()
        {
            if (!File.Exists("Patient_Information.Json"))
            {
                string CreatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
                File.WriteAllText("Patient_Information.Json", CreatePatientJson);
            }
            var patientList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));
            AllPatientInfo.AddRange(patientList);
        }

        [HttpPost]
        [Route("AddPatient")]
        //Add new patient to database
        public IHttpActionResult AddPatient(PatientDTO patient)
        {
            AllPatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

            //Add patient info to list
            if (patient != null)
            {
                AllPatientInfo.Add(MapToModel(patient));
                _config.IncreatementTotalPatientCounter();
                _config.IncreatementTotaBillCounter();

                string UpdatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
                File.WriteAllText("Patient_Information.Json", UpdatePatientJson);
                return Ok("New Patient added succcesfully.");
            }
            return BadRequest("Patient cannot be added now.");
        }
        [HttpPatch]
        [Route("AdmitPatient")]
        //Admit the patient to the respective department
        //Inclusive of consultation of doctors.
        public IHttpActionResult AdmitPatient(int ID, PatientVisitRecordDTO visitRecord)
        {
            Patient patientToAdmit = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();

            if(visitRecord != null)
            {
                patientToAdmit.VisitHistory.Add(MapToModel(visitRecord));

                string UpdatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
                File.WriteAllText("Patient_Information.Json", UpdatePatientJson);

                return Ok("Patient admitted successfully");
            }
            return BadRequest("Error admitting the patient");
        }

        [HttpPut]
        [Route("DischargePatient")]
        //Remove Patient from the List, but data still remain for future reference
        public IHttpActionResult DischargePatient(int ID, int BillID)
        {
            Patient PatientToDischarge = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
            List<Patient> DischargedPatients = new List<Patient>();

            for (int j = 0; j < PatientToDischarge.VisitHistory.Count; j++)
            {
                Bill TargetBill = PatientToDischarge.VisitHistory[j].BillInformation;
                if (TargetBill.BillID == BillID)
                {
                    if (TargetBill.Status == BillStatusEnum.Paid)
                    {
                        DischargedPatients.Add(PatientToDischarge);

                        string DischargedPaitentList = JsonConvert.SerializeObject(DischargedPatients);
                        File.WriteAllText("DischargedPatient.Json", DischargedPaitentList);

                        AllPatientInfo.Remove(PatientToDischarge);
                        string UpdatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
                        File.WriteAllText("Patient_Information.Json", UpdatePatientJson);

                        return Ok("Patient successfully discharged.");
                    }
                }
            }
            var UpdatedDischargedList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("DischargedPatient.Json"));
            DischargedPatients.AddRange(UpdatedDischargedList);

            return BadRequest("Patient cannot be discharged now.");
        }

        [HttpGet]
        [Route("ViewPatientInfo")]
        //Search through database for Patient's Information via Reference ID given to them
        public PatientDTO ViewPatientInfo(int ID)
        {
            IList<Patient> WritePatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));
            Patient TargetPatient = WritePatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();

            return MapToDTO(TargetPatient);
        }

        [HttpGet]
        [Route("CalculateTotalBill")]
        public double CalculateTotalBill(int ID, double subsidy)
        {
            Patient TargetPatient = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
            double TotalBill = 0;
            double FinalBill = 0;

            int nights = Convert.ToInt32(Console.ReadLine());

            double DeptPrice = GetDepartmentPrice(ID);
            double WardPrice = (GetWardPrice(ID) * nights);
            double MedPrice = GetMedicinePrice(ID);

            TotalBill = DeptPrice + WardPrice + MedPrice;
            double GST = TotalBill * _config.Gst;
            double subsidisedAmt=0;
            if (subsidy != 0)
            {
                subsidisedAmt = TotalBill * (subsidy / 100);
            }

            FinalBill = (TotalBill + GST) - subsidisedAmt;

            return FinalBill;
        }

        [HttpPatch]
        [Route("PayBill")]
        public void SettleBill(int ID, int BillID)
        {
            Patient BillPayment = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
            for (int i = 0; i < BillPayment.VisitHistory.Count; i++)
            {
                Bill TargetBill = BillPayment.VisitHistory[i].BillInformation;
                if (TargetBill.Status == BillStatusEnum.Unpaid)
                {
                    TargetBill.Status = BillStatusEnum.Paid;
                }
            }

            string UpdatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
            File.WriteAllText("Patient_Information.Json", UpdatePatientJson);

        }

        [HttpGet]
        [Route("GetPatientName")]
        public string GetPatientName(int ID)
        {
            //return NameOfPatient;
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().PatientName;

        }

        [HttpGet]
        [Route("GetPatientDept")]
        //Returns Department the patient is admitted to
        public DepartmentEnum GetPatientDept(int ID)
        {
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.FirstOrDefault().Department;
        }

        [HttpGet]
        [Route("GetPatientWard")]
        //Returns Ward the patient is admitted to
        public WardEnum GetPatientWard(int ID)
        {
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.FirstOrDefault().Ward;
        }
        [HttpGet]
        [Route("GetPatientMeds")]
        //Returns list of medicines from VisitHistory
        public List<string> GetPatientMedicineList(int ID)
        {
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.FirstOrDefault().ListOfMedicines;
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
            Patient pObj = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
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
            return new PatientVisitRecordDTO(record.DoctorInCharge,record.Department,record.Ward,record.StayDuration,record.ListOfSymptoms,record.ListOfMedicines, bidto);
        }
        private PatientVisitRecord MapToModel(PatientVisitRecordDTO record)
        {
            Bill bidto = MapToModel(record.BillInformation);
            return new PatientVisitRecord(record.DoctorInCharge, record.Department, record.Ward, record.StayDuration, record.ListOfSymptoms, record.ListOfMedicines, bidto);
        }

        private BillDTO MapToDTO(Bill bill)
        {
            return new BillDTO(bill.BillID, bill.GST, bill.Subsidy, bill.TotalAmount, bill.Status);
        }
        private Bill MapToModel(BillDTO bill)
        {
            return new Bill(bill.BillID, bill.GST, bill.Subsidy, bill.TotalAmount, bill.Status);
        }
    }
}
