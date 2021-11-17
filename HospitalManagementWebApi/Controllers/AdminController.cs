using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using Newtonsoft.Json;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController, IAdminController
    {
        List<Patient> AllPatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

        [HttpGet]
        [Route("PrintAllDepartments")]
        public (List<PatientDTO>, List<PatientDTO>, List<PatientDTO>) PrintAllDepartments()
        {
            List<Patient> OutPatients = new List<Patient>();
            List<Patient> InPatients = new List<Patient>();
            List<Patient> EmergencyPatients = new List<Patient>();

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                for(int j = 0; j < AllPatientInfo[i].VisitHistory.Count; j++)
                {
                    if (AllPatientInfo[i].VisitHistory[j].Department == DepartmentEnum.Outpatient)
                    {
                        if (!OutPatients.Contains(AllPatientInfo[i]))
                            OutPatients.Add(AllPatientInfo[i]);

                    }
                    else if (AllPatientInfo[i].VisitHistory[j].Department == DepartmentEnum.Inpatient)
                    {
                        if (!InPatients.Contains(AllPatientInfo[i]))
                            InPatients.Add(AllPatientInfo[i]);

                    }
                    else if (AllPatientInfo[i].VisitHistory[j].Department == DepartmentEnum.Emergency)
                    {
                        if (!EmergencyPatients.Contains(AllPatientInfo[i]))
                            EmergencyPatients.Add(AllPatientInfo[i]);

                    }

                }
            }
            List<PatientDTO> outPatientDTO = new List<PatientDTO>();
            foreach (Patient item in OutPatients)
                outPatientDTO.Add(MapToDTO(item));
            List<PatientDTO> inPatientDTO = new List<PatientDTO>();
            foreach (Patient item in InPatients)
                inPatientDTO.Add(MapToDTO(item));
            List<PatientDTO> emergencyPatientDTO = new List<PatientDTO>();
            foreach (Patient item in EmergencyPatients)
                emergencyPatientDTO.Add(MapToDTO(item));
            return (outPatientDTO, inPatientDTO, emergencyPatientDTO);
        }
        [HttpGet]
        [Route("PrintAllBIlls")]
        public List<BillDTO> PrintAllBills()
        {
            List<BillDTO> AllBills = new List<BillDTO>();
            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                for (int j = 0; j < AllPatientInfo[i].VisitHistory.Count; j++)
                {
                    AllBills.Add(MapToDTO(AllPatientInfo[i].VisitHistory[j].BillInformation));
                }
            }
            return AllBills;
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
            return new PatientVisitRecordDTO(record.DoctorInCharge, record.Department, record.Ward, record.StayDuration, record.ListOfSymptoms, record.ListOfMedicines, bidto);
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