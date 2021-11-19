using HospitalManagement.Common.Common;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using HospitalManagementWebApi.Utility;
using System.Collections.Generic;
using System.Data.Entity;

namespace HospitalManagementWebApi.DBContext
{
    public class HospitalDbInitializer : DropCreateDatabaseIfModelChanges<HospitalManagementDBContext>
    {
        public HospitalDbInitializer()
        {

        }

        protected override void Seed(HospitalManagementDBContext context)
        {
            base.Seed(context);
            List<User> UserAccounts = new List<User>();
            IUtilityManager utility = new UtilityManager();
            User AdminAccount = new User("Hospital Admin", 9999, utility.ComputeSha256Hash("Admin123"), UserType.Admin);
            User WorkerAccount = new User("Hospital Worker", 5555, utility.ComputeSha256Hash("Worker321"), UserType.Worker);
            UserAccounts.Add(AdminAccount);
            UserAccounts.Add(WorkerAccount);
            context.Users.AddRange(UserAccounts);
        }
    }
}