using HospitalManagement.Common.Common;
using System.Collections.Generic;

namespace HospitalManagement.Common.DTO
{
    public class PatientVisitRecordDTO
    {
        public DoctorsEnum DoctorInCharge { get; set; }     //Name of Doctor In Charge
        public DepartmentEnum Department { get; set; }         //Department the Patient is admitted to
        public WardEnum Ward { get; set; }               // Ward that the patient admitted to
        public int StayDuration { get; set; }               // How long the user stayed in the ward
        public List<string> ListOfSymptoms { get; set; }    //Reason the Patient is visiting the hospital for
        public List<string> ListOfMedicines { get; set; }   //Prescribed medicines
        public BillDTO BillInformation { get; set; }
        public PatientVisitRecordDTO(DoctorsEnum DoctorInCharge, DepartmentEnum Department, WardEnum Ward, int StayDuration,
                                    List<string> ListOfSymptoms, List<string> ListOfMedicines, BillDTO BillInformation)
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
