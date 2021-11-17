using HospitalManagement.Common.Common;

namespace HospitalManagement.Common.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string HashedPassword { get; set; }
        public UserType Type { get; set; }
        public UserDTO(string name, int id, string hasedPassword, UserType type)
        {
            Name = name;
            ID = id;
            HashedPassword = hasedPassword;
            Type = type;
        }
    }
}
