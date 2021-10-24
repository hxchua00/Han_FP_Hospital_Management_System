using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace Han_FP_Hospital_Management_System
{
    class StaffAccounts
    {
        public string Name { get; set; }
        public Guid UniqueID { get; set; }
        public int ID { get; set; }
        public string HashedPassword { get; set; }
        public static List<StaffAccounts> StaffAccountList = new List<StaffAccounts>();
        static StaffAccounts()
        {
            //Adding Admin account details
            string AdminPassword = "Admin123";

            Hospital_Admin AdminAccount = new Hospital_Admin()
            {
                Name = "Hospital Admin",
                UniqueID = Guid.NewGuid(),
                ID = 3001,
                HashedPassword = PasswordCreator.getHashedPassword(AdminPassword)
            };

            //Adding Admin account details
            string WorkerPassword = "Worker321";

            Hospital_Worker WorkerAccount = new Hospital_Worker()
            {
                Name = "Hospital Worker",
                UniqueID = Guid.NewGuid(),
                ID = 1001,
                HashedPassword = PasswordCreator.getHashedPassword(WorkerPassword)
            };

            //Adding both accounts to the list
            StaffAccountList.Add(AdminAccount);
            StaffAccountList.Add(WorkerAccount);

            string AddAccountToList = JsonConvert.SerializeObject(StaffAccountList);
            File.WriteAllText("Staff_Accounts.Json",AddAccountToList);

            List<StaffAccounts> AccountLists = JsonConvert.DeserializeObject<List<StaffAccounts>>(File.ReadAllText("Staff_Accounts.Json"));

            string SAList = JsonConvert.SerializeObject(AccountLists);
            File.WriteAllText("Staff_Accounts.Json", SAList);
        }
    }
}
