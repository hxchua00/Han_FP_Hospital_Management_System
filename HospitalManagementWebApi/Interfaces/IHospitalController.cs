using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace HospitalManagementWebApi.Interfaces
{
    public interface IHospitalController
    {
        void Initialize();
        IHttpActionResult AddPatient(PatientDTO p);
        PatientDTO AdmitPatient(int ID, PatientVisitRecordDTO visitRecord);
        IHttpActionResult DischargePatient(int ID, int BillID);
        PatientDTO ViewPatientInfo(int ID);
        double CalculateTotalBill(int ID, double subsidy);
        void SettleBill(int ID, int BillID);
        string GetPatientName(int ID);
        DepartmentEnum GetPatientDept(int ID);
        WardEnum GetPatientWard(int ID);
        List<string> GetPatientMedicineList(int ID);
        double GetDepartmentPrice(int ID);
        double GetWardPrice(int ID);
        double GetMedicinePrice(int ID);
        bool ValidatePatient(int ID);

    }
}
