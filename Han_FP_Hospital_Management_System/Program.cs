using System;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Han_FP_Hospital_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Welcome to the Hospital");
                Console.WriteLine();
                Console.WriteLine("1) Log in");
                Console.WriteLine("2) Exit\n");

                int Start = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Chosen option: {Start}\n");
                switch (Start)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Console.WriteLine("Thank you for using the Hospital Management System!");
                        Console.WriteLine("Goodbye!\n");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please select available option.\n");
                        break;
                }
            }

            Console.ReadLine();
        }

        //Start of the system, Login Page - Display different menu according to different users
        static void Login()
        {
            Console.WriteLine();
            Console.WriteLine("Pick your account type: ");
            Console.WriteLine("1) Patient");
            Console.WriteLine("2) Staff\n");

            int accType = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Chosen option: {accType}\n");

            switch (accType)
            {
                case 1:
                    CheckFirstVisit();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("Enter Staff ID: ");
                    int sID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    int result = ValidateStaffLogin(sID);
                    if(result == 1)
                    {
                        WorkerMenu(sID);
                    }
                    else if(result == 2)
                    {
                        AdminMenu(sID);
                    }
                    else
                    {
                        Console.WriteLine("ID not found! Please enter valid ID.\n");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option chosen.\n");
                    break;
            }
        }

        //Checks if patient has visited the hospital before or not
        //New patient will always go through Registration first
        static void CheckFirstVisit()
        {
            Hospital_Worker HW = new Hospital_Worker();

            try
            {
                Console.WriteLine();
                Console.WriteLine("Is this your first visit here?");
                Console.WriteLine("1) Yes");
                Console.WriteLine("2) No\n");

                int checkVisit = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Chosen option: {checkVisit}\n");
                if (checkVisit == 1)
                {
                    HW.AddPatient();
                }
                else if (checkVisit == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter ID number: ");
                    int input = Convert.ToInt32(Console.ReadLine());

                    for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
                    {
                        if (Hospital_Worker.AllPatientInfo[i].PatientID == input)
                        {
                            Console.WriteLine("Patient found!");
                            Hospital_Worker.AllPatientInfo[i].NumOfVisits++;
                            PatientMenu(input);
                            break;
                        }

                        if (i == Hospital_Worker.AllPatientInfo.Count - 1 && Hospital_Worker.AllPatientInfo[i].PatientID != input)
                        {
                            Console.WriteLine("Patient ID not found!");
                            Console.WriteLine("Please enter valid Patient ID!\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ID could not be found. Please enter a valid ID.\n");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid argument!\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Overflow detected!\n");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Not enough memory!\n");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid input format detected!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured!\n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }

        //Features that Patients that can do
        static void PatientMenu(int ID)
        {
            Hospital_Worker HW = new Hospital_Worker();
            bool loop = true;

            try
            {
                while (loop)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Welcome, {HW.GetPatientName(ID)}");
                    Console.WriteLine("What is your business today?\n");
                    Console.WriteLine("1) Consult Doctor");
                    Console.WriteLine("2) View Current Bill");
                    Console.WriteLine("3) Pay the bill");
                    Console.WriteLine("4) Nothing\n");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Chosen option: {choice}\n");

                    switch (choice)
                    {
                        case 1:
                            HW.AdmitPatient(ID);
                            break;
                        case 2:
                            HW.ShowTheBill(ID);
                            break;
                        case 3:
                            HW.SettleBill(ID);
                            break;
                        case 4:
                            Console.WriteLine("Thank you for coming! Stay safe and healthy!\n");
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please choose from the provided options.\n");
                            break;
                    }
                }
            }
            catch (MethodAccessException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! You do not have permission to access the current method!\n");
            }
            catch (MissingMethodException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! The method you are trying to access cannot be found!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid argument!\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Overflow detected!\n");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Not enough memory!\n");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid input format detected!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured!\n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }

        static int ValidateStaffLogin(int sID)
        {
            Console.WriteLine("Enter Staff Password: ");
            string sPassword = Console.ReadLine();

            int result = 0;
            for (int i = 0; i < StaffAccounts.StaffAccountList.Count; i++)
            {
                if (StaffAccounts.StaffAccountList[i].ID == sID &&
                    PasswordCreator.verifyHashedPassword(StaffAccounts.StaffAccountList[i].HashedPassword, sPassword)
                    == PasswordVerificationResult.Success)
                {
                    if (sID == 1001)
                        result = 1;
                    else if (sID == 3001)
                        result = 2;
                }
            }

            return result;
        }

        static void WorkerMenu(int ID)
        {
            Hospital_Worker HW = new Hospital_Worker();
            int patientID;
            bool loop = true;
            try
            {
                while (loop)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Using Account ID: {ID}");
                    Console.WriteLine("Please choose what to do: ");
                    Console.WriteLine("1) Add Patient");
                    Console.WriteLine("2) Find Patient");
                    Console.WriteLine("3) Generate Bill for patient");
                    Console.WriteLine("4) Discharge Patient");
                    Console.WriteLine("5) Nothing\n");

                    int option = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Chosen option: {option}\n");

                    switch (option)
                    {
                        case 1:
                            HW.AddPatient();
                            break;
                        case 2:
                            Console.WriteLine("Please enter Patient's ID: \n");
                            patientID = Convert.ToInt32(Console.ReadLine());
                            HW.ViewPatientInfo(patientID);
                            break;
                        case 3:
                            Console.WriteLine("Enter patient's ID here: ");
                            patientID = Convert.ToInt32(Console.ReadLine());
                            HW.GenerateBill(patientID);
                            break;
                        case 4:
                            Console.WriteLine("Enter patient's ID here: ");
                            patientID = Convert.ToInt32(Console.ReadLine());
                            HW.DischargePatient(patientID);
                            break;
                        case 5:
                            Console.WriteLine("Taking a break so soon? There's still many things to do.\n");
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please choose from the provided options.\n");
                            break;
                    }
                }
            }
            catch (MethodAccessException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! You do not have permission to access the current method!\n");
            }
            catch (MissingMethodException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! The method you are trying to access cannot be found!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid argument!\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Overflow detected!\n");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Not enough memory!\n");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid input format detected!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured!\n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }
        static void AdminMenu(int ID)
        {
            Hospital_Admin HA = new Hospital_Admin();

            bool loop = true;

            try
            {
                while (loop)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Using Account ID: {ID}, Welcome!");
                    Console.WriteLine("Please choose what to do: \n");
                    Console.WriteLine("1) View All Patients in all Departments");
                    Console.WriteLine("2) View All bills generated");
                    Console.WriteLine("3) Nothing\n");

                    int option = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Chosen option: {option}\n");

                    switch (option)
                    {
                        case 1:
                            HA.PrintPatientDept();
                            break;
                        case 2:
                            HA.PrintAllBills();
                            break;
                        case 3:
                            Console.WriteLine("Taking a break so soon? There's still many things to do.\n");
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please choose from the provided options.\n");
                            break;

                    }
                }
            }
            catch (MethodAccessException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! You do not have permission to access the current method!\n");
            }
            catch (MissingMethodException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! The method you are trying to access cannot be found!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid argument!\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Overflow detected!\n");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Not enough memory!\n");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Invalid input format detected!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured!\n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }

        }
    }
}
