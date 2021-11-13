namespace HospitalManagementWebApi.Interfaces
{
    public interface IConfigurationController
    {
        int TotalPatientRegistration { get; }

        int TotalBillCounter { get; }
        double Gst { get; }
        void IncreatementTotalPatientCounter();
        void IncreatementTotaBillCounter();
    }
}
