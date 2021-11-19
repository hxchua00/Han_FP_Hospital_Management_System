using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace HospitalManagementWebApi.Interfaces
{
    public interface IWorkerController
    {
        IHttpActionResult AddPatient(PatientDTO p);
        IHttpActionResult AdmitPatient(int ID, PatientVisitRecordDTO visitRecord);
        IHttpActionResult ViewPatientInfo(int ID);
        double CalculateTotalBill(int ID, double subsidy);
        IHttpActionResult SettleBill(int ID, int BillID);
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
