using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Common
{
    public class PatientVisitRecordDTO
    {
        public DoctorsEnum DoctorInCharge { get; private set; }     //Name of Doctor In Charge
        public DepartmentEnum Department { get; private set; }         //Department the Patient is admitted to
        public WardEnum Ward { get; private set; }               // Ward that the patient admitted to
        public int StayDuration { get; private set; }               // How long the user stayed in the ward
        public List<string> ListOfSymptoms { get; private set; }    //Reason the Patient is visiting the hospital for
        public List<string> ListOfMedicines { get; private set; }   //Prescribed medicines
        public BillDTO BillInformation { get; private set; }

        public PatientVisitRecordDTO() { }
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
