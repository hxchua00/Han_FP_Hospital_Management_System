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
            Console.WriteLine("Welcome to the Hospital");
            Console.WriteLine();
            Console.WriteLine("1) Log in");
            Console.WriteLine("2) Exit\n");
            int Start = Convert.ToInt32(Console.ReadLine());
            bool cont = true;
            while (cont)
            {
                switch (Start)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please select available option.\n");
                        break;
                }
            }
                        
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
                    CheckFirstVisit();
                    break;
                case 2:
                    StaffMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option chosen.");
                    break;
            }
        }

        static void CheckFirstVisit()
        {
            Patient newPatient = new Patient();

            Console.WriteLine("Is this your first visit here?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No\n");
            int checkVisit = Convert.ToInt32(Console.ReadLine());
            if (checkVisit == 1)
            {
                Hospital_Worker HW = new Hospital_Worker();
                HW.AddPatient(newPatient);
            }
            else if(checkVisit == 2)
            {
                Console.WriteLine("Please enter your name: ");
            }
            else
            {
                Console.WriteLine("Invalid input!\n");
            }
        }

        static void PatientMenu()
        {
            
        }

        static void StaffMenu()
        {

        }
    }
}
