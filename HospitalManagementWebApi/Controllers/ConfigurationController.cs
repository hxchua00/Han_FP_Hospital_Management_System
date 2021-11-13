using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HospitalManagementWebApi.Controllers
{
    public class ConfigurationController : ApiController
    {
        public int TotalPatientRegistration { get; private set; }
        public int TotalBillCounter { get; private set; }
        public double Gst { get; private set; }
        public ConfigurationController() { }

        public void IncreatementTotalPatientCounter()
        {
            TotalPatientRegistration++;
        }
        public void IncreatementTotaBillCounter()
        {
            TotalBillCounter++;
        }
        public ConfigurationController(int patientCounter, int billCounter, double gst)
        {
            TotalPatientRegistration = patientCounter;
            TotalBillCounter = billCounter;
            Gst = gst;
        }
    }
}