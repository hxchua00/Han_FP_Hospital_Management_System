using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Han_FP_Hospital_Management_System
{
    public class UtilityManager : IUtilityManager
    {
        //Hashing method using SHA256
        public string ComputeSha256Hash(string rawData)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
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
            }
            catch (TargetInvocationException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Methods cannot be invoked through reflection!");
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Unable to cast to type. Overflow detected!");
            }
            catch (EncoderFallbackException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Encoder fall back operation has failed!");
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Object has already been disposed!");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid format detected!");

            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invoked argument is out of range!");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference argument detected!");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Unknown exception caught!");
            }
            return builder.ToString();
        }
    }
}
