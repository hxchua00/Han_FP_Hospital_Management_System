using System.Collections.Generic;

namespace Han_FP_Hospital_Management_System
{
    public interface IConfigurationManager
    {
        int TotalPatientRegistration { get; }

        int TotalBillCounter { get; }
        double Gst { get; }
        void IncreatementTotalPatientCounter();
        void IncreatementTotaBillCounter();
    }
}
