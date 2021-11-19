using HospitalManagementWebApi.Models;
using System.Data.Entity;

namespace HospitalManagementWebApi.DBContext
{
    public class HospitalManagementDBContext : DbContext
    {
        public HospitalManagementDBContext() : base()
        {
            Database.SetInitializer(new HospitalDbInitializer());
        }

        public DbSet<Patient> PatientList { get; set; }
        public DbSet<PatientVisitRecord> VisitRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; } 
    }
}