using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalManagement.Common.Common;

namespace HospitalManagementWebApi.Models
{
    [Table("VisitRecords")]
    public class PatientVisitRecord
    {
        [Key]
        public int RecordNumber { get; private set; }
        public DoctorsEnum DoctorInCharge { get; private set; }     //Name of Doctor In Charge
        public DepartmentEnum Department { get; private set; }         //Department the Patient is admitted to
        public WardEnum Ward { get; private set; }               // Ward that the patient admitted to
        public List<string> ListOfSymptoms { get; private set; }    //Reason the Patient is visiting the hospital for
        public List<string> ListOfMedicines { get; private set; }   //Prescribed medicines
        public Bill BillInformation { get; private set; }
        public int PatientID { get;}

        public PatientVisitRecord() { }
        public PatientVisitRecord(int RecordNumber, DoctorsEnum DoctorInCharge, DepartmentEnum Department, WardEnum Ward,
                                    List<string> ListOfSymptoms, List<string> ListOfMedicines, Bill BillInformation, int PatientID)
        {
            this.RecordNumber = RecordNumber;
            this.DoctorInCharge = DoctorInCharge;
            this.Department = Department;
            this.Ward = Ward;
            this.ListOfSymptoms = ListOfSymptoms;
            this.ListOfMedicines = ListOfMedicines;
            this.BillInformation = BillInformation;
            this.PatientID = PatientID;
        }
    }
}
