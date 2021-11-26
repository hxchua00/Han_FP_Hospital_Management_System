using HospitalManagement.Common.Common;
using System.Collections.Generic;

namespace HospitalManagement.Common.DTO
{
    public class PatientDTO
    {
        public int PatientID { get; set; }                              //Unique ID for patients
        public string PatientName { get; set; }                         //Name of Patient
        public double PatientWeight { get; set; }                       //Weight of Patient
        public double PatientHeight { get; set; }                       //Height of Patient
        public GenderEnum PatientGender { get; set; }                   //Patient's gender
        public int PatientAge { get; set; }                             //Age of Patient
        public EthenicityEnum Ethnicity { get; set; }                   //Race of the Patient
        public string Address { get; set; }                             //Patient's Current Staying Address 
        public int ContactNum { get; set; }                             //Patient's contact number
        public List<PatientVisitRecordDTO> VisitHistory { get; set; }
        public PatientDTO() { }

        public PatientDTO(int totalRegistration, string pName, EthenicityEnum pEthnicity, double pHeight,
            double pWeight, GenderEnum pGender, string pAddress, int pContactNum, int pAge)
        {
            PatientID = 1000 + totalRegistration;
            PatientName = pName;
            Ethnicity = pEthnicity;
            PatientHeight = pHeight;
            PatientWeight = pWeight;
            PatientGender = pGender;
            Address = pAddress;
            ContactNum = pContactNum;
            PatientAge = pAge;
            VisitHistory = new List<PatientVisitRecordDTO>();
        }
        public void AddPatientVisitRecord(PatientVisitRecordDTO record)
        {
            VisitHistory.Add(record);
        }
        public void AddPatientVisitInformation(List<PatientVisitRecordDTO> record)
        {
            VisitHistory = record;
        }
    }
}
