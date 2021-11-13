using HospitalManagement.Common.DTO;

namespace HospitalManagementWebApi.Interfaces
{
    //Interface for UserManager Class
    public interface IUserController
    {
        UserDTO CurrentUser { get; }
        void Initialize();
        UserDTO GetUser(int userID);
        bool LogOn(int userId, string password);
    }
}
