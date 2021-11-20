using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using System.Collections.Generic;

namespace Han_FP_Hospital_Management_System.Interfaces
{
    interface IHospitalManagementViewModel
    {
        PatientDTO AddPatient(PatientDTO patient);
        PatientVisitRecordDTO AdmitPatient(int ID, PatientVisitRecordDTO visitRecord);
        PatientDTO ViewPatientInfo(int ID);
        BillDTO GenerateBill(int ID);
        BillDTO SettleBill(int ID, int BillID);
        string GetPatientName(int ID);
        DepartmentEnum GetPatientDept(int ID);
        WardEnum GetPatientWard(int ID);
        ICollection<string> GetPatientMedicineList(int ID);
        int GetPatientVisitRecordsCount(int ID);
        double GetDepartmentPrice(int ID);
        double GetWardPrice(int ID);
        double GetMedicinePrice(int ID);
        bool ValidatePatient(int ID);
        ICollection<PatientDTO> GetPatientByDepartment(DepartmentEnum departmentType);
        ICollection<BillDTO> PrintAllBills();
        UserDTO GetUser(int userID);
        bool LogOn(int userId, string password);
    }
}
