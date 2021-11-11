using System.Collections.Generic;

namespace Han_FP_Hospital_Management_System
{
    interface IHospitalManager
    {
        void Initialize();
        void UpdatePatientJsonFile();
        void AddPatient(Patient p);
        void AdmitPatient(int ID, PatientVisitRecord visitRecord);
        void DischargePatient(int ID);
        void ViewPatientInfo(int ID);
        double CalculateTotalBill(int ID);
        void SettleBill(int ID);
        Bill GenerateBill(int ID);
        void ShowTheBill(int ID);
        string GetPatientName(int ID);
        DepartmentEnum GetPatientDept(int ID);
        WardEnum GetPatientWard(int ID);
        List<string> GetPatientMedicineList(int ID);
        double GetDepartmentPrice(int ID);
        double GetWardPrice(int ID);
        double GetMedicinePrice(int ID);
        bool ValidatePatient(int ID);
        void PrintAllDepartments();
        void PrintAllBills();

    }
}
