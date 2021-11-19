using System.Web.Http;

namespace HospitalManagementWebApi.Interfaces
{
    public interface IConfigurationController
    {
        int TotalPatientRegistration { get; }

        int TotalBillCounter { get; }
        double Gst { get; }
        IHttpActionResult IncreatementTotalPatientCounter();
        IHttpActionResult IncreatementTotaBillCounter();
    }
}
