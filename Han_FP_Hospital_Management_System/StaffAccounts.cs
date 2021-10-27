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

            try
            {
                string AddAccountToList = JsonConvert.SerializeObject(StaffAccountList);
                File.WriteAllText("Staff_Accounts.Json", AddAccountToList);

                List<StaffAccounts> AccountLists = JsonConvert.DeserializeObject<List<StaffAccounts>>(File.ReadAllText("Staff_Accounts.Json"));

                string SAList = JsonConvert.SerializeObject(AccountLists);
                File.WriteAllText("Staff_Accounts.Json", SAList);
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Error! Method is not supported!\n");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error! Access is unauthorized!\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error! File not found in specified directory!\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Error! Length of path or filename is too long!\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error! Null reference pointer detected from given argument!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Error! Argument provided is invalid!\n");
            }
            catch (IOException)
            {
                Console.WriteLine("Error! I/O Error has occured! \n");
            }
            catch (Exception)
            {
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }  
        }
    }
}
