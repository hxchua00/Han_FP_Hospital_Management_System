using HospitalManagement.Common.DTO;
using HospitalManagementWebApi.DBContext;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using HospitalManagementWebApi.Utility;
using System;
using System.Linq;
using System.Web.Http;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserController : ApiController, IUserController
    {
        HospitalManagementDBContext HMdBContext;
        private IUtilityManager _utility;
        
        public UserController()
        {
            _utility = new UtilityManager();
            HMdBContext = new HospitalManagementDBContext();
        }

        [HttpGet]
        [Route("VerifyLogin")]
        //Login verification, checking aginst data read from the Json file
        public bool LogOn(int userId, string password)
        {
            User userObj = HMdBContext.Users.Where(x => x.ID == userId).FirstOrDefault();
            return string.Equals(userObj.HashedPassword, _utility.ComputeSha256Hash(password), StringComparison.Ordinal);
        }

        [HttpGet]
        [Route("GetUser")]
        //Returns user Information whose userID is defined
        public UserDTO GetUser(int userID)
        {
            return MapToDTO(HMdBContext.Users.Where(x => x.ID == userID).FirstOrDefault());
        }
       
        private UserDTO MapToDTO(User user)
        {
            return new UserDTO(user.Name,user.ID,user.HashedPassword,user.Type);
        }
        private User MapToModel(UserDTO user)
        {
            return new User(user.Name, user.ID, user.HashedPassword, user.Type);
        }
    }
}