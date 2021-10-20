using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hospital_Admin HA = new Hospital_Admin();
            //HA.CreateWorker();

            Hospital_Worker HW = new Hospital_Worker();
            HW.ViewPatientInfo();

            Console.ReadLine();
        }

        static void Login()
        {
            Console.WriteLine("Pick your account type: ");
            Console.WriteLine("1) Patient");
            Console.WriteLine("2) Staff");
            Console.WriteLine();
            int accType = Convert.ToInt32(Console.ReadLine());
            switch (accType)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Invalid option chosen.");
                    break;
            }
        }
    }
}
