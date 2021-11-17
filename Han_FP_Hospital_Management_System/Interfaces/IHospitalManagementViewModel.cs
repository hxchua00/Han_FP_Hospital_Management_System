using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using System.Collections.Generic;

namespace Han_FP_Hospital_Management_System.Interfaces
{
    interface IHospitalManagementViewModel
    {
        void Initialize();
        PatientDTO AddPatient(PatientDTO patient);
        PatientVisitRecordDTO AdmitPatient(int ID, PatientVisitRecordDTO visitRecord);
        PatientDTO DischargePatient(int ID, int BillID);
        PatientDTO ViewPatientInfo(int ID);
        double CalculateTotalBill(int ID);
        BillDTO SettleBill(int ID, int BillID);
        string GetPatientName(int ID);
        DepartmentEnum GetPatientDept(int ID);
        WardEnum GetPatientWard(int ID);
        ICollection<string> GetPatientMedicineList(int ID);
        double GetDepartmentPrice(int ID);
        double GetWardPrice(int ID);
        double GetMedicinePrice(int ID);
        bool ValidatePatient(int ID);
        ICollection<PatientDTO> PrintAllDepartments();
        ICollection<BillDTO> PrintAllBills();
        UserDTO GetUser(int userID);
        bool LogOn(int userId, string password);
    }
}
