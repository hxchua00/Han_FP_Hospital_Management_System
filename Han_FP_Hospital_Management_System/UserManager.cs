using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Han_FP_Hospital_Management_System
{
    class UserManager
    {
        public static List<User> UserAccounts = new List<User>();

        static UserManager()
        {
            User AdminAccount = new User()
            {
                Name = "Hospital Admin",
                ID = 9999,
                HashedPassword = ComputeSha256Hash("Admin123")
            };

            //Adding Admin account details
            User WorkerAccount = new User()
            {
                Name = "Hospital Worker",
                ID = 1000,
                HashedPassword = ComputeSha256Hash("Worker321")
            };

            UserAccounts.Add(AdminAccount);
            UserAccounts.Add(WorkerAccount);

            string AddAccountToList = JsonConvert.SerializeObject(UserAccounts);
            File.WriteAllText("Staff_Accounts.Json", AddAccountToList);

            List<User> AccountLists = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Staff_Accounts.Json"));

            string UserList = JsonConvert.SerializeObject(AccountLists);
            File.WriteAllText("Staff_Accounts.Json", UserList);
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
