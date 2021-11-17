using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using HospitalManagement.Common.Common;
using HospitalManagement.Common.DTO;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using HospitalManagementWebApi.Utility;
using Newtonsoft.Json;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserController : ApiController, IUserController
    {
        private IUtilityManager _utility;
        private List<User> _accountLists = new List<User>();
        public UserDTO CurrentUser { get; private set; }

        public UserController()
        {
            _utility = new UtilityManager();
            AddUser();
            Initialize();
        }

        //Runs at start of program
        public void Initialize()
        {
            //Creates the Json File, if missing, then adds the User accounts in
            if (!File.Exists("Staff_Accounts.Json"))
                AddUser();
            //Read into the Json File and get the information
            _accountLists = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Staff_Accounts.Json"));
            CurrentUser = null;
        }

        [HttpGet]
        [Route("VerifyLogin")]
        //Login verification, checking aginst data read from the Json file
        public bool LogOn(int userId, string password)
        {
            User userObj = _accountLists.Where(x => x.ID == userId).FirstOrDefault();
            return string.Equals(userObj.HashedPassword, _utility.ComputeSha256Hash(password), StringComparison.Ordinal);
        }

        [HttpPost]
        [Route("Adduser")]
        //Creates new User accounts 
        private void AddUser()
        {
            List<User> UserAccounts = new List<User>();

            //Admin Account
            User AdminAccount = new User("Hospital Admin", 9999, _utility.ComputeSha256Hash("Admin123"), UserType.Admin);

            //WorkerAccount
            User WorkerAccount = new User("Hospital Worker", 5555, _utility.ComputeSha256Hash("Worker321"), UserType.Worker);

            //Adds the accounts to the List
            UserAccounts.Add(AdminAccount);
            UserAccounts.Add(WorkerAccount);

            //Writes account details into the Json File
            string AddAccountToList = JsonConvert.SerializeObject(UserAccounts);
            File.WriteAllText("Staff_Accounts.Json", AddAccountToList);
        }

        [HttpGet]
        [Route("GetUser")]
        //Returns user Information whose userID is defined
        public UserDTO GetUser(int userID)
        {
            return MapToDTO(_accountLists.Where(x => x.ID == userID).FirstOrDefault());
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