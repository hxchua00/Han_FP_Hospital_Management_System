

namespace Han_FP_Hospital_Management_System
{
    class Users
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string HashedPassword { get; set; }

        public Users()
        {
            
        }

        public Users(string Name, int ID, string HashedPassword)
        {
            this.Name = Name;
            this.ID = ID;
            this.HashedPassword = HashedPassword;
        }
    }
}
