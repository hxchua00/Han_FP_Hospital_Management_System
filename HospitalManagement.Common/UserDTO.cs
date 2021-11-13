﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Common
{
    public class UserDTO
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public string HashedPassword { get; private set; }
        public UserType Type { get; private set; }
        public UserDTO() { }
        public UserDTO(string name, int id, string hasedPassword, UserType type)
        {
            Name = name;
            ID = id;
            HashedPassword = hasedPassword;
            Type = type;
        }
    }
}
