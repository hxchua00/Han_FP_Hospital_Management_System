

namespace Han_FP_Hospital_Management_System
{
    class Users
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string HashedPassword { get; set; }

        Hospital_Admin AdminAccount;
        Hospital_Worker WorkerAccount;

        public Users()
        {
            AdminAccount = new Hospital_Admin()
            {
                Name = "Hospital Admin",
                ID = 9999,
                HashedPassword = PasswordCreator.getHashedPassword("Admin123")
            };

            //Adding Admin account details
            WorkerAccount = new Hospital_Worker()
            {
                Name = "Hospital Worker",
                ID = 1000,
                HashedPassword = PasswordCreator.getHashedPassword("Worker321")
            };

            UserManager.UserAccounts.Add(AdminAccount);
            UserManager.UserAccounts.Add(WorkerAccount);
        }
    }
}
