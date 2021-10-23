using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System
{
    static class PasswordCreator
    {
        private static PasswordHasher _workerPW_Hasher;

        static PasswordCreator()
        {
            _workerPW_Hasher = new PasswordHasher();
        }

        public static string getHashedPassword(string pwd)
        {
            return _workerPW_Hasher.HashPassword(pwd);
        }

        public static PasswordVerificationResult verifyHashedPassword(string hashedPwd,string pwd)
        {
            return _workerPW_Hasher.VerifyHashedPassword(hashedPwd,pwd);
        }

    }
}
