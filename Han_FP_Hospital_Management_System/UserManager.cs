using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security;

namespace Han_FP_Hospital_Management_System
{
    class UserManager
    {
        public static List<Users> UserAccounts = new List<Users>();

        static UserManager()
        {
            Users AdminAccount = new Users()
            {
                Name = "Hospital Admin",
                ID = 9999,
                HashedPassword = PasswordCreator.getHashedPassword("Admin123")
            };

            //Adding Admin account details
            Users WorkerAccount = new Users()
            {
                Name = "Hospital Worker",
                ID = 1000,
                HashedPassword = PasswordCreator.getHashedPassword("Worker321")
            };

            UserAccounts.Add(AdminAccount);
            UserAccounts.Add(WorkerAccount);

            string AddAccountToList = JsonConvert.SerializeObject(UserAccounts);
            File.WriteAllText("Staff_Accounts.Json", AddAccountToList);

            List<Users> AccountLists = JsonConvert.DeserializeObject<List<Users>>(File.ReadAllText("Staff_Accounts.Json"));

            string UserList = JsonConvert.SerializeObject(AccountLists);
            File.WriteAllText("Staff_Accounts.Json", UserList);
        }
    }
}
