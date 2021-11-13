using System.Collections.Generic;

namespace HospitalManagementWebApi.Interfaces
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
