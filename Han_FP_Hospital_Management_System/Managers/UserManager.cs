using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Han_FP_Hospital_Management_System
{
    public class UserManager : IUserManager
    {
        private IUtilityManager _utility;
        private List<User> _accountLists = new List<User>();
        public User CurrentUser { get; private set; }
        public UserManager(IUtilityManager utility)
        {
            //Creating an interface object
            _utility = utility;
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
        //Login verification, checking aginst data read from the Json file
        public bool LogOn(int userId, string password)
        {
            User userObj = _accountLists.Where(x => x.ID == userId).FirstOrDefault();
            return string.Equals(userObj.HashedPassword, _utility.ComputeSha256Hash(password), StringComparison.Ordinal);
        }
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

        //Returns user Information whose userID is defined
        public User GetUser(int userID)
        {
            return _accountLists.Where(x => x.ID == userID).FirstOrDefault();
        }

    }
}
