using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using Han_FP_Hospital_Management_System;

namespace HospitalManagementWebApi.Controllers
{
    [RoutePrefix("api/Authentication")]
    public class UtilityController : ApiController, IUtilityManager
    {
        [HttpGet]
        [Route("")]
        public string ComputeSha256Hash(string rawData)
        {
            StringBuilder builder = new StringBuilder();
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

            }
           
            return builder.ToString();
        }
    }
}