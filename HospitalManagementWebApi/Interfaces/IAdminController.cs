using HospitalManagement.Common.DTO;
using System.Collections.Generic;

namespace HospitalManagementWebApi.Interfaces
{
    interface IAdminController
    {
        (List<PatientDTO>, List<PatientDTO>, List<PatientDTO>) PrintAllDepartments();
        List<BillDTO> PrintAllBills();
    }
}
