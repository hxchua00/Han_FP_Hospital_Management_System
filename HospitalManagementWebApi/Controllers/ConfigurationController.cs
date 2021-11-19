using HospitalManagementWebApi.Interfaces;
using System.Web.Http;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Config")]
    public class ConfigurationController : ApiController, IConfigurationController
    {
        public int TotalPatientRegistration { get; private set; }
        public int TotalBillCounter { get; private set; }
        public double Gst { get; private set; }
        public ConfigurationController() : this(0,0,0.07) { }

        public ConfigurationController(int patientCounter, int billCounter, double gst)
        {
            TotalPatientRegistration = patientCounter;
            TotalBillCounter = billCounter;
            Gst = gst;
        }

        [HttpPatch]
        [Route("IncreasePatientCount")]
        public IHttpActionResult IncreatementTotalPatientCounter()
        {
            TotalPatientRegistration++;
            return Ok("Total Patient Count has increased by 1");
        }
        [HttpPatch]
        [Route("IncreaseBillCount")]
        public IHttpActionResult IncreatementTotaBillCounter()
        {
            TotalBillCounter++;
            return Ok("Total Bill Count has increased by 1");
        }
    }
}