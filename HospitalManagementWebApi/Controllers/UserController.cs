using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Han_FP_Hospital_Management_System;
using Newtonsoft.Json;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserController : ApiController, IUserManager
    {
        private IUtilityManager _utility;
        private List<User> _accountLists = new List<User>();
        public User CurrentUser { get; private set; }

        [HttpGet]
        [Route("")]
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

        [HttpPut]
        [Route("CreateUser")]
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
        public User GetUser(int userID)
        {
            return _accountLists.Where(x => x.ID == userID).FirstOrDefault();
        }

    }
}