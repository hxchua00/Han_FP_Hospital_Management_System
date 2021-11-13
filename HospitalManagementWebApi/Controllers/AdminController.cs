using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using HospitalManagement.Common;
using HospitalManagementWebApi.Models;
using Newtonsoft.Json;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        List<Patient> AllPatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

        [HttpGet]
        [Route("PrintAllDepartments")]
        public (List<Patient>, List<Patient>, List<Patient>) PrintAllDepartments()
        {
            List<Patient> OutPatients = new List<Patient>();
            List<Patient> InPatients = new List<Patient>();
            List<Patient> EmergencyPatients = new List<Patient>();

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                for(int j = 0; j < AllPatientInfo[i].VisitHistory.Count; j++)
                {
                    if (AllPatientInfo[i].VisitHistory[j].Department == DepartmentEnum.Outpatient)
                    {
                        if (!OutPatients.Contains(AllPatientInfo[i]))
                            OutPatients.Add(AllPatientInfo[i]);

                    }
                    else if (AllPatientInfo[i].VisitHistory[j].Department == DepartmentEnum.Inpatient)
                    {
                        if (!InPatients.Contains(AllPatientInfo[i]))
                            InPatients.Add(AllPatientInfo[i]);

                    }
                    else if (AllPatientInfo[i].VisitHistory[j].Department == DepartmentEnum.Emergency)
                    {
                        if (!EmergencyPatients.Contains(AllPatientInfo[i]))
                            EmergencyPatients.Add(AllPatientInfo[i]);

                    }

                }
            }
            return (OutPatients, InPatients, EmergencyPatients);
        }
        [HttpGet]
        [Route("PrintAllBIlls")]
        public List<Bill> PrintAllBills()
        {
            List<Bill> AllBills = new List<Bill>();
            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                for (int j = 0; j < AllPatientInfo[i].VisitHistory.Count; j++)
                {
                    AllBills.Add(AllPatientInfo[i].VisitHistory[j].BillInformation);
                }
            }
            return AllBills;
        }
    }
}