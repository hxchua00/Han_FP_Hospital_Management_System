using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using HospitalManagementWebApi.DBContext;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController, IAdminController
    {
        private HospitalManagementDBContext HMdBContext;
        public AdminController()
        {
            HMdBContext = new HospitalManagementDBContext();
        }

        [HttpGet]
        [Route("GetPatientByDepartment")]
        public IHttpActionResult GetPatientByDepartment(DepartmentEnum departmentType)
        {
            IEnumerable<Patient> patientList = HMdBContext.PatientList.ToList();
            List<PatientDTO> AllPatient = new List<PatientDTO>();
            if(patientList.Count() > 0)
            {
                foreach (Patient patient in patientList)
                {
                    if (patient.VisitHistory.FirstOrDefault().Department == departmentType)
                    {
                        AllPatient.Add(MapToDTO(patient));
                    }
                }
                return Ok(AllPatient);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("PrintAllBIlls")]
        public IHttpActionResult PrintAllBills()
        {
            IEnumerable<BillDTO> AllBills = new List<BillDTO>();
            foreach (Bill bill in HMdBContext.Bills.ToList())
                AllBills.Append(MapToDTO(bill));
            if (AllBills.Count() > 0)
                return Ok(AllBills);
            else
                return NotFound();
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
        private PatientVisitRecordDTO MapToDTO(PatientVisitRecord record)
        {
            BillDTO bidto = MapToDTO(record.BillInformation);
            return new PatientVisitRecordDTO(record.DoctorInCharge, record.Department, record.Ward, record.StayDuration, record.ListOfSymptoms, record.ListOfMedicines, bidto);
        }
        private BillDTO MapToDTO(Bill bill)
        {
            return new BillDTO(bill.BillID, bill.GST, bill.Subsidy, bill.TotalAmount, bill.Status);
        }
    }
}