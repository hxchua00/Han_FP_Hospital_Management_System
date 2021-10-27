using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            StringBuilder builder = new StringBuilder();
            try
            {
                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Convert byte array to a string   
                    
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    
                }
            }
            catch (TargetInvocationException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Methods cannot be invoked through reflection!");
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Unable to cast to type. Overflow detected!");
            }
            catch (EncoderFallbackException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Encoder fall back operation has failed!");
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Object has already been disposed!");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid format detected!");

            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invoked argument is out of range!");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference argument detected!");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Unknown exception caught!");
            }
            return builder.ToString();
        }
    }
}
