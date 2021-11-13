﻿using System.Collections.Generic;
using HospitalManagement.Common;

namespace HospitalManagementWebApi.Models
{
    public class PatientVisitRecord
    {
        public DoctorsEnum DoctorInCharge { get; private set; }     //Name of Doctor In Charge
        public DepartmentEnum Department { get; private set; }         //Department the Patient is admitted to
        public WardEnum Ward { get; private set; }               // Ward that the patient admitted to
        public int StayDuration { get; private set; }               // How long the user stayed in the ward
        public List<string> ListOfSymptoms { get; private set; }    //Reason the Patient is visiting the hospital for
        public List<string> ListOfMedicines { get; private set; }   //Prescribed medicines
        public Bill BillInformation { get; private set; }

        public PatientVisitRecord() { }
        public PatientVisitRecord(DoctorsEnum DoctorInCharge, DepartmentEnum Department, WardEnum Ward, int StayDuration,
                                    List<string> ListOfSymptoms, List<string> ListOfMedicines, Bill BillInformation)
        {
            this.DoctorInCharge = DoctorInCharge;
            this.Department = Department;
            this.Ward = Ward;
            this.StayDuration = StayDuration;
            this.ListOfSymptoms = ListOfSymptoms;
            this.ListOfMedicines = ListOfMedicines;
            this.BillInformation = BillInformation;
        }
    }
}
