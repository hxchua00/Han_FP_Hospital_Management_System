namespace HospitalManagementWebApi.Interfaces
{
    //Interfeace class for UtilityManager
    public interface IUtilityManager
    {
        string ComputeSha256Hash(string rawData);
    }
}
