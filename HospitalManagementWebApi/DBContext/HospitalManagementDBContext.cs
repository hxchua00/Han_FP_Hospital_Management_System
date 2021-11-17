using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementWebApi.Models;
using System.Data.Entity;

namespace HospitalManagementWebApi.DBContext
{
    public class HospitalManagementDBContext : DbContext
    {
        public HospitalManagementDBContext() : base()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<HospitalManagementDBContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> PatientList { get; set; }
        public DbSet<PatientVisitRecord> VisitRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}