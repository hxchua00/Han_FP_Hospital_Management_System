using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HospitalManagementWebApi.DBContext;
using HospitalManagementWebApi.Interfaces;
using HospitalManagementWebApi.Models;
using HospitalManagementWebApi.Utility;
using HospitalManagement.Common.Common;

namespace HospitalManagementWebApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HospitalManagementDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(HospitalManagementDBContext HMcontext)
        {
            IUtilityManager _utility = new UtilityManager();

            //Admin Account
            User AdminAccount = new User("Hospital Admin", 9999, _utility.ComputeSha256Hash("Admin123"), UserType.Admin);
            //WorkerAccount
            User WorkerAccount = new User("Hospital Worker", 5555, _utility.ComputeSha256Hash("Worker321"), UserType.Worker);

            HMcontext.Users.Add(WorkerAccount);
            HMcontext.Users.Add(AdminAccount);

            HMcontext.SaveChanges();
        }
    }
}