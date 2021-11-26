using HospitalManagement.Common.Common;
using System.Collections.Generic;

namespace HospitalManagement.Common.DTO
{
    public class PatientVisitRecordDTO
    {
        public int ID { get; set; }  
        public DoctorsEnum DoctorInCharge { get; set; }     //Name of Doctor In Charge
        public DepartmentEnum Department { get; set; }         //Department the Patient is admitted to
        public WardEnum Ward { get; set; }               // Ward that the patient admitted to
        public List<string> ListOfSymptoms { get; set; }    //Reason the Patient is visiting the hospital for
        public List<string> ListOfMedicines { get; set; }   //Prescribed medicines
        public BillDTO BillInformation { get; set; }

        public PatientDTO PatientID { get; set; }

        public PatientVisitRecordDTO(int ID, DoctorsEnum DoctorInCharge, DepartmentEnum Department, WardEnum Ward,
                                    List<string> ListOfSymptoms, List<string> ListOfMedicines, BillDTO BillInformation)
        {
            this.DoctorInCharge = DoctorInCharge;
            this.Department = Department;
            this.Ward = Ward;
            this.ListOfSymptoms = ListOfSymptoms;
            this.ListOfMedicines = ListOfMedicines;
            this.BillInformation = BillInformation;
        }
    }
}
