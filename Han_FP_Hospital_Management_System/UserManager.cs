using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security;

namespace Han_FP_Hospital_Management_System
{
    class UserManager : Users
    {
        public static List<Users> UserAccounts = new List<Users>();

        static UserManager()
        {
            string AddAccountToList = JsonConvert.SerializeObject(UserAccounts);
            File.WriteAllText("Staff_Accounts.Json", AddAccountToList);

            List<Users> AccountLists = JsonConvert.DeserializeObject<List<Users>>(File.ReadAllText("Staff_Accounts.Json"));

            string UserList = JsonConvert.SerializeObject(AccountLists);
            File.WriteAllText("Staff_Accounts.Json", UserList);
        }
    }
}
