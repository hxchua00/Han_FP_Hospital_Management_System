using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Han_FP_Hospital_Management_System
{
    class Program
    {
        private static IUserManager userManager;
        private static IUtilityManager utility;
        private static IHospitalManager hospitalManager;
        private static IConfigurationManager _config;
        static void Main(string[] args)
        {

            utility = new UtilityManager();
            userManager = new UserManager(utility);
            if (!File.Exists("Configuration.Json"))
            {   
                ConfigurationManager config = new ConfigurationManager(0, 0, 0.7);
                string configJson = JsonConvert.SerializeObject(config);
                File.WriteAllText("Configuration.Json", configJson);
            }
            _config = JsonConvert.DeserializeObject<ConfigurationManager>(File.ReadAllText("Configuration.Json"));

            hospitalManager = new HospitalManager(_config);
            userManager.Initialize();
            hospitalManager.Initialize();

            try
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

        //Start of the system, Login Page - Display different menu according to different users
        static void Login()
        {
            try
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
                        Console.WriteLine("Enter ID: ");
                        int sID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Password: ");
                        string sPassword = Console.ReadLine();

                        bool logonResult= userManager.LogOn(sID, sPassword);
                        if(logonResult)
                        {
                            switch(userManager.CurrentUser.Type)
                            {
                                case UserType.Admin:
                                    AdminMenu(userManager.CurrentUser.ID);
                                    break;
                                case UserType.Worker:
                                    WorkerMenu(userManager.CurrentUser.ID);
                                    break;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option chosen.\n");
                        break;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! There is nothing in the File!\n");
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

        //Checks if patient has visited the hospital before or not
        //New patient will always go through Registration first
        static void CheckFirstVisit()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Is this the patient's first time visiting?");
                Console.WriteLine("1) Yes");
                Console.WriteLine("2) No\n");

                int checkVisit = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Chosen option: {checkVisit}\n");
                if (checkVisit == 1)
                {
                    Patient p = AddNewPatientDetails();
                    hospitalManager.AddPatient(p);
                }
                else if (checkVisit == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter ID number: ");
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (hospitalManager.ValidatePatient(input))
                    {
                        PatientMenu(input);
                    }
                    else
                    {
                        Console.WriteLine("Patient ID not found!");
                        Console.WriteLine("Please enter valid Patient ID!\n");
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
            bool loop = true;
            try
            {
                while (loop)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Welcome, {hospitalManager.GetPatientName(ID)}");
                    Console.WriteLine("What is your business today?\n");
                    Console.WriteLine("1) See the doctor");
                    Console.WriteLine("2) Pay the bill");
                    Console.WriteLine("3) Nothing\n");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Chosen option: {choice}\n");
                    DoctorsEnum DocInCharge;
                    switch (choice)
                    {
                        case 1:
                            //Set Doctor in charge
                            Console.WriteLine("Who is the doctor in charge?");
                            Console.WriteLine("1) Mark");
                            Console.WriteLine("2) Lee");
                            Console.WriteLine("3) Kayle");
                            Console.WriteLine("4) Aisha");
                            Console.WriteLine("5) Amelia");
                            Console.WriteLine("6) Reno\n");
                            int Doctors = Convert.ToInt32(Console.ReadLine());
                            switch (Doctors)
                            {
                                case 1:
                                    DocInCharge = DoctorsEnum.Mark;
                                    break;
                                case 2:
                                    DocInCharge = DoctorsEnum.Lee;
                                    break;
                                case 3:
                                    DocInCharge = DoctorsEnum.Kayle;
                                    break;
                                case 4:
                                    DocInCharge = DoctorsEnum.Aisha;
                                    break;
                                case 5:
                                    DocInCharge = DoctorsEnum.Amelia;
                                    break;
                                case 6:
                                    DocInCharge = DoctorsEnum.Reno;
                                    break;
                                default:
                                    Console.WriteLine("Doctor does not exists!\n");
                                    DocInCharge = DoctorsEnum.Invalid;
                                    break;
                            }
                            bool cont = true;
                            List<string> Symptoms = new List<string>();
                            //Set Patient's symptoms
                            while (cont)
                            {
                                Console.WriteLine("What symptoms does the patient have?");
                                Console.WriteLine("1) Flu");
                                Console.WriteLine("2) Cough");
                                Console.WriteLine("3) Fever");
                                Console.WriteLine("4) Rashes");
                                Console.WriteLine("5) Headache");
                                Console.WriteLine("6) Stomachache");
                                Console.WriteLine("7) Indigestion");
                                Console.WriteLine("8) Vomitting");
                                Console.WriteLine("9) Allergy");
                                Console.WriteLine("0) Nothing else\n");
                                int symptom = Convert.ToInt32(Console.ReadLine());
                                
                                switch (symptom)
                                {
                                    case 1:
                                        Symptoms.Add("Flu");
                                        break;
                                    case 2:
                                        Symptoms.Add("Cough");
                                        break;
                                    case 3:
                                        Symptoms.Add("Fever");
                                        break;
                                    case 4:
                                        Symptoms.Add("Rashes");
                                        break;
                                    case 5:
                                        Symptoms.Add("Headache");
                                        break;
                                    case 6:
                                        Symptoms.Add("Stomachache");
                                        break;
                                    case 7:
                                        Symptoms.Add("Indigestion");
                                        break;
                                    case 8:
                                        Symptoms.Add("Vomitting");
                                        break;
                                    case 9:
                                        Symptoms.Add("Allergy");
                                        break;
                                    case 0:
                                        cont = false;
                                        break;
                                    default:
                                        Console.WriteLine("Invalid option! Please choose from the avaliable options only.\n");
                                        break;
                                }      
                            }
                            //Set Patient's Medicines
                            List<string> Medicines = new List<string>();
                            while (cont)
                            {
                                Console.WriteLine("What medicines are the patient prescribed?");
                                Console.WriteLine("1) Paracetamol");
                                Console.WriteLine("2) Antibiotics");
                                Console.WriteLine("3) Painkillers");
                                Console.WriteLine("4) AntiInflammation");
                                Console.WriteLine("5) Antihistamine");
                                Console.WriteLine("6) Lozenges");
                                Console.WriteLine("0) Nothing else\n");
                                int meds = Convert.ToInt32(Console.ReadLine());
                                
                                switch (meds)
                                {
                                    case 1:
                                        Medicines.Add("Paracetamol");
                                        break;
                                    case 2:
                                        Medicines.Add("Antibiotics");
                                        break;
                                    case 3:
                                        Medicines.Add("Painkillers");
                                        break;
                                    case 4:
                                        Medicines.Add("AntiInflammation");
                                        break;
                                    case 5:
                                        Medicines.Add("Antihistamine");
                                        break;
                                    case 6:
                                        Medicines.Add("Lozenges");
                                        break;
                                    case 0:
                                        cont = false;
                                        break;
                                    default:
                                        Console.WriteLine("Invalid option! Please choose from the avaliable options only.\n");
                                        break;
                                }
                            }
                            //Set Patient's Department
                            Console.WriteLine("Which department is the patient admitted to?");
                            Console.WriteLine("1) Outpatient");
                            Console.WriteLine("2) Inpatient");
                            Console.WriteLine("3) Emergency\n");
                            int dept = Convert.ToInt32(Console.ReadLine());
                            DepartmentEnum department;
                            switch (dept)
                            {
                                case 1:
                                    department = DepartmentEnum.Outpatient;
                                    break;
                                case 2:
                                    department = DepartmentEnum.Inpatient;
                                    break;
                                case 3:
                                    department = DepartmentEnum.Emergency;
                                    break;
                                default:
                                    Console.WriteLine("Department not unavailable!\n");
                                    department = DepartmentEnum.Invalid;
                                    break;
                            }
                            //Set Patient's Ward
                            Console.WriteLine("Which ward is the patient admitted to?");
                            Console.WriteLine("1) Class A");
                            Console.WriteLine("2) Class B");
                            Console.WriteLine("3) Class C");
                            Console.WriteLine("4) Private");
                            Console.WriteLine("5) Not admitted to any ward\n");
                            int wardchoice = Convert.ToInt32(Console.ReadLine());

                            WardEnum ward;
                            int duration = 0;

                            switch (wardchoice)
                            {
                                case 1:
                                    ward = WardEnum.ClassA;
                                    Console.WriteLine("Number of nights stayed in the ward: ");
                                    duration = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 2:
                                    ward = WardEnum.ClassB;
                                    Console.WriteLine("Number of nights stayed in the ward: ");
                                    duration = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 3:
                                    ward = WardEnum.ClassC;
                                    Console.WriteLine("Number of nights stayed in the ward: ");
                                    duration = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 4:
                                    ward = WardEnum.Private;
                                    Console.WriteLine("Number of nights stayed in the ward: ");
                                    duration = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 5:
                                    ward = WardEnum.NIL;
                                    break;
                                default:
                                    Console.WriteLine("Ward does not exists!");
                                    ward = WardEnum.Invalid;
                                    break;
                            }

                            //Generating Bill
                            Console.WriteLine("Generating bill for patient...\n");

                            int BillID = _config.TotalBillCounter;
                            double totalAmt = hospitalManager.CalculateTotalBill(ID);
                            double gst = _config.Gst;
                            Console.WriteLine("How much subsidies does the patient have? (%)");
                            double subsidy = Convert.ToDouble(Console.ReadLine());
                            BillStatusEnum status = BillStatusEnum.Unpaid;

                            Bill newBill = new Bill(BillID, gst, subsidy, totalAmt, status);


                            //Calls AdmitPatient
                            if (department == DepartmentEnum.Invalid || ward == WardEnum.Invalid || DocInCharge == DoctorsEnum.Invalid)
                            {
                                Console.WriteLine("Invalid information detected. Unable to proceed with data recording.\n");
                            }
                            else
                            {
                                Console.WriteLine($"Patient's Bill ID for this visit is, {BillID}.");
                                Console.WriteLine($"Use this ID to pay your bills, or else you won't be discharged.\n");

                                PatientVisitRecord newRecord = new PatientVisitRecord(DocInCharge, department, ward, duration, Symptoms, Medicines, newBill);
                                hospitalManager.AdmitPatient(ID, newRecord);
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter bill ID to settle bill: ");
                            int billID = Convert.ToInt32(Console.ReadLine());
                            hospitalManager.SettleBill(ID, billID);
                            break;
                        case 3:
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
        static void WorkerMenu(int ID)
        {            
            try
            {
                int patientID;
                bool loop = true;

                while (loop)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Using Account ID: {ID}, Welcome back!");
                    Console.WriteLine("Please choose what to do: ");
                    Console.WriteLine("1) Add Patient");
                    Console.WriteLine("2) Find Patient");
                    Console.WriteLine("3) Generate Bill for patient");
                    Console.WriteLine("3) Discharge Patient");
                    Console.WriteLine("4) Nothing\n");

                    int option = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Chosen option: {option}\n");

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Adding new patient information...\n");
                            Patient p = AddNewPatientDetails();
                            hospitalManager.AddPatient(p);
                            break;
                        case 2:
                            Console.WriteLine("Enter Patient's ID: ");
                            patientID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                            Console.WriteLine("Searching for patient...\n");

                            Console.WriteLine("==============================");
                            hospitalManager.ViewPatientInfo(patientID);
                            Console.WriteLine("==============================");
                            break;
                        case 3:
                            Console.Write("Enter patient ID: ");
                            patientID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                           
                            break;
                        case 4:
                            Console.WriteLine("Enter patient's ID here: ");
                            patientID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Bill ID for this visit: ");
                            int billID = Convert.ToInt32(Console.ReadLine());
                            hospitalManager.DischargePatient(patientID,billID);
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
            try
            {
                bool loop = true;

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
                            hospitalManager.PrintAllDepartments();
                            break;
                        case 2:
                            hospitalManager.PrintAllBills();
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

        static Patient AddNewPatientDetails()
        {
            Console.WriteLine("Please enter the details of the patients.\n");
            Console.WriteLine("Enter patient's name: ");
            string pName = Console.ReadLine();
            Console.WriteLine("Enter patient's age: ");
            int pAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter patient's height: ");
            double pHeight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter patient's weight: ");
            double pWeight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter patient's gender: ");
            Console.WriteLine("1) Male");
            Console.WriteLine("2) Female");
            Console.WriteLine("3) Prefer not to say");
            int gender = Convert.ToInt32(Console.ReadLine());
            GenderEnum pGender;
            switch (gender)
            {
                case 1:
                    pGender = GenderEnum.Male;
                    break;
                case 2:
                    pGender = GenderEnum.Female;
                    break;
                case 3:
                    pGender = GenderEnum.Undisclosed;
                    break;
                default:
                    Console.WriteLine("Invalid input! Please choose a valid option.");
                    pGender = GenderEnum.Invalid;
                    break;
            }

            Console.WriteLine("Enter patient's ethnicity: ");
            Console.WriteLine("1) Chinese");
            Console.WriteLine("2) Malay");
            Console.WriteLine("3) Indian");
            Console.WriteLine("4) Eurasian");
            Console.WriteLine("5) Indonesian");
            Console.WriteLine("6) Japanese");
            Console.WriteLine("7) Korean");
            Console.WriteLine("8) Taiwanese");
            Console.WriteLine("9) Others");
            int ethnicity = Convert.ToInt32(Console.ReadLine());
            EthenicityEnum pEthnicity;
            switch (ethnicity)
            {
                case 1:
                    pEthnicity = EthenicityEnum.Chinese;
                    break;
                case 2:
                    pEthnicity = EthenicityEnum.Malay;
                    break;
                case 3:
                    pEthnicity = EthenicityEnum.Indian;
                    break;
                case 4:
                    pEthnicity = EthenicityEnum.Eurasian;
                    break;
                case 5:
                    pEthnicity = EthenicityEnum.Indonesian;
                    break;
                case 6:
                    pEthnicity = EthenicityEnum.Japanese;
                    break;
                case 7:
                    pEthnicity = EthenicityEnum.Korean;
                    break;
                case 8:
                    pEthnicity = EthenicityEnum.Taiwanese;
                    break;
                case 9:
                    pEthnicity = EthenicityEnum.Others;
                    break;
                default:
                    Console.WriteLine("Invalid input! Please choose a valid option.");
                    pEthnicity = EthenicityEnum.Invalid;
                    break;
            }

            Console.WriteLine("Enter patient's contact number: ");
            int pContactNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter patient's address: ");
            string pAddress = Console.ReadLine();

            int totalRegistration = _config.TotalPatientRegistration;

            Patient newPatient;

            if (pName == "" || pHeight == 0 || pWeight == 0 || pContactNum == 0 || pAddress == "" ||
                pEthnicity == EthenicityEnum.Invalid || pGender == GenderEnum.Invalid)
            {
                Console.WriteLine("Registration unsuccessful! Please provide all valid information required.\n");
                newPatient = null;
            }
            else
            {
                newPatient = new Patient(totalRegistration, pName, pEthnicity, pHeight,
                                            pWeight, pGender, pAddress, pContactNum, pAge);
                
            }
            return newPatient;
        }
    }
}
