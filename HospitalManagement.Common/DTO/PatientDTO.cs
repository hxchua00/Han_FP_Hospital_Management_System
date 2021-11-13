﻿using HospitalManagement.Common.Common;
using System.Collections.Generic;

namespace HospitalManagement.Common.DTO
{
    public class PatientDTO
    {
        public int PatientID { get; private set; }                              //Unique ID for patients
        public string PatientName { get; private set; }                         //Name of Patient
        public double PatientWeight { get; private set; }                       //Weight of Patient
        public double PatientHeight { get; private set; }                       //Height of Patient
        public GenderEnum PatientGender { get; private set; }                   //Patient's gender
        public int PatientAge { get; private set; }                             //Age of Patient
        public EthenicityEnum Ethnicity { get; private set; }                   //Race of the Patient
        public string Address { get; private set; }                             //Patient's Current Staying Address 
        public int ContactNum { get; private set; }                             //Patient's contact number
        public List<PatientVisitRecordDTO> VisitHistory { get; private set; }

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
    }
}
