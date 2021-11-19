using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalManagement.Common.Common;

namespace HospitalManagementWebApi.Models
{
    [Table("PatientList")]
    public class Patient
    {
        
        [Key]
        public int PatientID { get; set; }                  //Unique ID for patients
        public string PatientName { get; set; }             //Name of Patient
        public double PatientWeight { get; set; }           //Weight of Patient
        public double PatientHeight { get; set; }           //Height of Patient
        public GenderEnum PatientGender { get; set; }           //Patient's gender
        public int PatientAge { get; set; }                 //Age of Patient
        public EthenicityEnum Ethnicity { get; set; }               //Race of the Patient
        public string Address { get; set; }                 //Patient's Current Staying Address 
        public int ContactNum { get; set; }                 //Patient's contact number
        public List<PatientVisitRecord> VisitHistory { get; set; }

        public Patient() { }

        public Patient(int totalRegistration, string pName, EthenicityEnum pEthnicity, double pHeight,
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
            VisitHistory = new List<PatientVisitRecord>();   
        }

        public void AddPatientVisitInformation(PatientVisitRecord record)
        {
            VisitHistory.Add(record);
        }
        public void AddPatientVisitInformation(List<PatientVisitRecord> record)
        {
            VisitHistory = record;
        }
    }
       
}
