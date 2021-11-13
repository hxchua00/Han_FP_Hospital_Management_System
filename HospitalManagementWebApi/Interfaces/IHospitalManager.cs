﻿using System.Collections.Generic;
using HospitalManagement.Common;
using HospitalManagement.Common.Common;
using HospitalManagementWebApi.Models;

namespace HospitalManagementWebApi.Interfaces
{
    public interface IHospitalManager
    {
        void Initialize();
        void AddPatient(Patient p);
        void AdmitPatient(int ID, PatientVisitRecord visitRecord);
        void DischargePatient(int ID, int BillID);
        void ViewPatientInfo(int ID);
        double CalculateTotalBill(int ID);
        void SettleBill(int ID, int BillID);
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
