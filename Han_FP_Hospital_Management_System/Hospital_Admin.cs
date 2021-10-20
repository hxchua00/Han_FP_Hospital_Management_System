using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace Han_FP_Hospital_Management_System
{
    class Hospital_Admin
    {
        public Guid AdminID { get; private set; }
        public string HashedPassword { get; private set; }

        List<Hospital_Admin> StaffAccounts = new List<Hospital_Admin>();
        static Hospital_Admin()
        {
            string AdminPassword = "Admin123";
            var AdminPW_Hasher = new PasswordHasher();

            Hospital_Admin Admin = new Hospital_Admin()
            {
                AdminID = Guid.NewGuid(),
                HashedPassword = AdminPW_Hasher.HashPassword(AdminPassword),
            };

            string AdminPWJson = JsonConvert.SerializeObject(Admin);
            File.AppendAllText("Staff_Accounts.Json", AdminPWJson);
        }
        public void CreateWorker()
        {
            Console.WriteLine("Enter a password for worker: ");
            string WorkerPassword = Console.ReadLine();

            var WorkerPW_Hasher = new PasswordHasher();
            Hospital_Worker newWorker = new Hospital_Worker(Guid.NewGuid(), WorkerPW_Hasher.HashPassword(WorkerPassword));
            
            string WorkerPWJson = JsonConvert.SerializeObject(newWorker);
            File.AppendAllText("Staff_Accounts.Json", WorkerPWJson);
        }

        //Prints all patients in each department
        public void PrintPatientDept()
        {

        }

        //Prints total number of bills geneerated
        public void PrintAllBills()
        {

        }
    }
}
