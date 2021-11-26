using HospitalManagement.Common.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementWebApi.Models
{
    public class User
    {
        [Key]
        public int ID { get; private set; }
        public string Name { get; private set; }
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
