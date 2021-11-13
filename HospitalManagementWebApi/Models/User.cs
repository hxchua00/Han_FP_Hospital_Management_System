using HospitalManagement.Common.Common;

namespace HospitalManagementWebApi.Models
{
    public class User
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public string HashedPassword { get; private set; }
        public UserType Type { get; private set; }
        public User() { }
        public User(string name, int id, string hasedPassword, UserType type)
        {
            Name = name;
            ID = id;
            HashedPassword = hasedPassword;
            Type = type;
        }
    }
}
