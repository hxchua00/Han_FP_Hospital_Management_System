using HospitalManagement.Common.Common;
using System.Web.Http;

namespace HospitalManagementWebApi.Interfaces
{
    interface IAdminController
    {
        IHttpActionResult GetPatientByDepartment(DepartmentEnum departmentType);
        IHttpActionResult PrintAllBills();
    }
}
