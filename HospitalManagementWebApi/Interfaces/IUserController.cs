using HospitalManagement.Common.DTO;

namespace HospitalManagementWebApi.Interfaces
{
    //Interface for UserManager Class
    public interface IUserController
    {
        UserDTO GetUser(int userID);
        bool LogOn(int userId, string password);
    }
}
