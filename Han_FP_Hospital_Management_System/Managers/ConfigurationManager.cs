using System.Collections.Generic;

namespace Han_FP_Hospital_Management_System
{
    public class ConfigurationManager : IConfigurationManager
    {
        public int TotalPatientRegistration { get; private set; }
        public int TotalBillCounter { get; private set; }
        public double Gst { get; private set; }
        public ConfigurationManager() { }

        public void IncreatementTotalPatientCounter()
        {
            TotalPatientRegistration++;
        }
        public void IncreatementTotaBillCounter()
        {
            TotalBillCounter++;
        }
        public ConfigurationManager(int patientCounter, int billCounter, double gst) 
        {
            TotalPatientRegistration = patientCounter;
            TotalBillCounter = billCounter;
            Gst = gst;
        }
    }
}
