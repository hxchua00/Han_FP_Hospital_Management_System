using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Newtonsoft.Json;


namespace Han_FP_Hospital_Management_System
{
    class Hospital_Worker 
    {
        public static List<Patient> AllPatientInfo = new List<Patient>();    //Stores all pateint information
        public static List<Bill> AllBills = new List<Bill>();                //Stores all bills

        //Keeps a record of all illnees the patient came to the hospital for
        public static  Dictionary<int, List<string>> IllnessRecord = new Dictionary<int, List<string>>();  
        
        static Hospital_Worker() 
        {
            try
            {
                if (!File.Exists("Patient_Information.Json"))
                {
                    string CreatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
                    File.WriteAllText("Patient_Information.Json", CreatePatientJson);
                }

                if (!File.Exists("Bills.Json"))
                {
                    string CreateBillJson = JsonConvert.SerializeObject(AllBills);
                    File.WriteAllText("Bills.Json", CreateBillJson);
                }

                var patientList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));
                AllPatientInfo.AddRange(patientList);

                var billList = JsonConvert.DeserializeObject<List<Bill>>(File.ReadAllText("Bills.Json"));
                AllBills.AddRange(billList);
            }
            catch (FileLoadException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File cannot be loaded successfully!\n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File you are trying to access cannot be found!\n");
            }
            catch (SecurityException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Security breached!\n");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Method is not supported!\n");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Access is unauthorized!\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File not found in specified directory!\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Length of path or filename is too long!\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference pointer detected from given argument!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Argument provided is invalid!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured! \n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }

        public void UpdateBillJsonFile()
        {
            //Update new information about Bills
            string CreateBillJson = JsonConvert.SerializeObject(AllBills);
            File.WriteAllText("Bills.Json", CreateBillJson);

            List<Bill> BillList = JsonConvert.DeserializeObject<List<Bill>>(File.ReadAllText("Bills.Json"));

            string bList = JsonConvert.SerializeObject(BillList);
            File.WriteAllText("Bills.Json", bList);
        }

        //Updates Patient Information
        public void UpdatePatientJsonFile()
        {
            //Update new information about patients
            string CreatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
            File.WriteAllText("Patient_Information.Json", CreatePatientJson);

            List<Patient> OldPatientList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

            string pList = JsonConvert.SerializeObject(OldPatientList);
            File.WriteAllText("Patient_Information.Json", pList);

        }

        //Add new patient to database
        public void AddPatient()
        {
            try
            {
                AllPatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

                string pList = JsonConvert.SerializeObject(AllPatientInfo);
                File.WriteAllText("Patient_Information.Json", pList);

                Patient newPatient = new Patient();

                newPatient = newPatient.Registration();
                //Add patient info to list
                if (newPatient != null)
                {
                    AllPatientInfo.Add(newPatient);

                    Console.WriteLine($"Name: {newPatient.PatientName}, ID: {newPatient.PatientID} has been added successfully!\n");
                    Console.WriteLine("If you are a patient, please re-login using the new ID provided for you!\n");
                }
                else
                    Console.WriteLine("Registering of new patient failed. Please try again.\n");

                UpdatePatientJsonFile();
            }
            catch (FileLoadException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File cannot be loaded successfully!\n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File you are trying to access cannot be found!\n");
            }
            catch (SecurityException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Security breached!\n");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Method is not supported!\n");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Access is unauthorized!\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File not found in specified directory!\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Length of path or filename is too long!\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference pointer detected from given argument!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Argument provided is invalid!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured! \n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }

        //Admit the patient to the respective department
        //Inclusive of consultation of doctors.
        public void AdmitPatient(int ID)
        {
            try
            {
                var lst = new List<string>();
                while (true)
                {
                    Console.WriteLine("Enter patient's Symptoms: ");
                    string Symptom = Console.ReadLine();
                    lst.Add(Symptom);

                    Console.WriteLine();
                    Console.WriteLine("Do you want to add more Symptoms: ");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No\n");
                    int res = Int32.Parse(Console.ReadLine());

                    if (res == 2)
                    {
                        break;
                    }
                }

                IllnessRecord[ID] = lst;

                Console.WriteLine("Which department is the patient admitted to?");
                Console.WriteLine("1) Outpatient");
                Console.WriteLine("2) Critical Care");
                Console.WriteLine("3) A&E");
                Console.WriteLine("4) Theraphy\n");

                int department = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                string dept;
                switch (department)
                {
                    case 1:
                        dept = "Outpatient";
                        break;
                    case 2:
                        dept = "Critical Care";
                        break;
                    case 3:
                        dept = "A&E";
                        break;
                    case 4:
                        dept = "Therapy";
                        break;
                    default:
                        dept = "";
                        break;
                }

                Console.WriteLine("Which ward is the patient admitted to?");
                Console.WriteLine("1) Class A");
                Console.WriteLine("2) Class B");
                Console.WriteLine("3) Class C");
                Console.WriteLine("4) Private");
                Console.WriteLine("5) Not admitting to ward\n");

                int WardClass = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                string ward;
                switch (WardClass)
                {
                    case 1:
                        ward = "Class A";
                        break;
                    case 2:
                        ward = "Class B";
                        break;
                    case 3:
                        ward = "Class C";
                        break;
                    case 4:
                        ward = "Private";
                        break;
                    case 5:
                        ward = "Unassigned";
                        break;
                    default:
                        Console.WriteLine("Invalid input, default 'Unassigned' will be assigned");
                        ward = "Unassigned";
                        break;
                }

                var Meds = new List<string>();
                while (true)
                {
                    Console.WriteLine("Prescribing medicine to patient: ");
                    Console.WriteLine("1) Paracetamol");
                    Console.WriteLine("2) Antibiotics");
                    Console.WriteLine("3) AntiInflammation");
                    Console.WriteLine("4) Antihistamines");
                    Console.WriteLine("5) Antacids");
                    Console.WriteLine("6) Painkillers\n");

                    int prescribtion = Convert.ToInt32(Console.ReadLine());
                    string medicine;

                    if (prescribtion == 1)
                    {
                        medicine = "Paracetamol";
                        Meds.Add(medicine);
                    }
                    else if (prescribtion == 2)
                    {
                        medicine = "Antibiotics";
                        Meds.Add(medicine);
                    }
                    else if (prescribtion == 3)
                    {
                        medicine = "AntiInflammation";
                        Meds.Add(medicine);
                    }
                    else if (prescribtion == 4)
                    {
                        medicine = "Antihistamines";
                        Meds.Add(medicine);
                    }
                    else if (prescribtion == 5)
                    {
                        medicine = "Antacids";
                        Meds.Add(medicine);
                    }
                    else if (prescribtion == 6)
                    {
                        medicine = "Painkillers";
                        Meds.Add(medicine);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Are there any more medicine to be prescribed?: ");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No\n");
                    int res = Int32.Parse(Console.ReadLine());

                    if (res == 2)
                    {
                        break;
                    }
                }

                //Updating the remaining details to Patient's information
                for (int i = 0; i < AllPatientInfo.Count; i++)
                {
                    if (AllPatientInfo[i].PatientID == ID)
                    {
                        AllPatientInfo[i].ListOfMedicines = Meds;
                        AllPatientInfo[i].ListOfSymptoms = lst;
                        AllPatientInfo[i].Department = dept;
                        AllPatientInfo[i].WardClass = ward;
                        Console.WriteLine($"{AllPatientInfo[i].PatientName} has been admitted to {dept}.");
                        if (ward != "Unassigned")
                        {
                            Console.WriteLine($"{AllPatientInfo[i].PatientName} has been admitted to Ward {ward}.\n");
                        }
                        else
                        {
                            Console.WriteLine($"{AllPatientInfo[i].PatientName} is not admitted to any ward.\n");
                        }
                    }
                }

                UpdatePatientJsonFile();
            }
            catch (FileLoadException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File cannot be loaded successfully!\n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File you are trying to access cannot be found!\n");
            }
            catch (SecurityException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Security breached!\n");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Method is not supported!\n");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Access is unauthorized!\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File not found in specified directory!\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Length of path or filename is too long!\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference pointer detected from given argument!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Argument provided is invalid!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured! \n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }
        
        //Remove Patient from the List, but data still remain for future reference
        public void DischargePatient(int ID)
        {
            try
            {
                for (int i = 0; i < AllPatientInfo.Count; i++)
                {
                    if (AllPatientInfo[i].PatientID == ID)
                    {
                        if (AllPatientInfo[i].BillPayment == true)
                        {
                            AllPatientInfo[i].Department = "Discharged";
                            AllPatientInfo[i].WardClass = "Discharged";
                        }
                        else
                        {
                            Console.WriteLine($"{AllPatientInfo[i].PatientName} has yet to pay the bill!");
                            Console.WriteLine("Discharge is allowed only after the bill has been paid!\n");
                        }
                    }
                }

                UpdatePatientJsonFile();
            }
            catch (FileLoadException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File cannot be loaded successfully!\n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File you are trying to access cannot be found!\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File not found in specified directory!\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Length of path or filename is too long!\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference pointer detected from given argument!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Argument provided is invalid!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured! \n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }

        //Search through database for Patient's Information via Reference ID given to them
        public void ViewPatientInfo(int ID)
        {
            try
            {
                List<Patient> WritePatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

                for (int i = 0; i < WritePatientInfo.Count; i++)
                {
                    if (WritePatientInfo[i].PatientID == ID)
                    {
                        Console.WriteLine($"Patient's ID: {WritePatientInfo[i].PatientID}");
                        Console.WriteLine($"Patient's Name: {WritePatientInfo[i].PatientName}\n");

                        Console.WriteLine($"Ethnicity: {WritePatientInfo[i].Ethnicity}");
                        Console.WriteLine($"Age: {WritePatientInfo[i].PatientAge}");
                        Console.WriteLine($"Weight: {WritePatientInfo[i].PatientWeight}");
                        Console.WriteLine($"Height: {WritePatientInfo[i].PatientHeight}\n");

                        Console.WriteLine($"Address: {WritePatientInfo[i].Address}");
                        Console.WriteLine($"Contact Number: {WritePatientInfo[i].ContactNum}\n");

                        Console.WriteLine($"Doctor In-charge: {WritePatientInfo[i].DoctorInCharge}");
                        Console.WriteLine($"Department: {WritePatientInfo[i].Department}");
                        Console.WriteLine($"Ward: {WritePatientInfo[i].WardClass}\n");

                        for (int j = 0; j < WritePatientInfo[i].ListOfSymptoms.Count; j++)
                        {
                            Console.WriteLine($"Symptom {j + 1}: {WritePatientInfo[i].ListOfSymptoms[j]}");
                        }

                        Console.WriteLine();

                        for (int j = 0; j < WritePatientInfo[i].ListOfMedicines.Count; j++)
                        {
                            Console.WriteLine($"Prescribed medicine {j + 1}: {WritePatientInfo[i].ListOfMedicines[j]}");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (FileLoadException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File cannot be loaded successfully!\n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File you are trying to access cannot be found!\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! File not found in specified directory!\n");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Length of path or filename is too long!\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Null reference pointer detected from given argument!\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! Argument provided is invalid!\n");
            }
            catch (IOException)
            {
                Console.WriteLine();
                Console.WriteLine("Error! I/O Error has occured! \n");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error! An exception has occured! Check your codes again!\n");
            }
        }

        //Returns Patient name
        public string GetPatientName(int ID)
        {
            string NameOfPatient = "";

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if(AllPatientInfo[i].PatientID == ID)
                {
                    NameOfPatient = AllPatientInfo[i].PatientName;
                }
            }

            return NameOfPatient;
        }
        //Returns Department the patient is admitted to
        public string GetPatientDept(int ID)
        {
            string DeptOfPatient = "";

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    DeptOfPatient = AllPatientInfo[i].Department;
                }
            }

            return DeptOfPatient;
        }
        //Returns Ward the patient is admitted to
        public string GetPatientWard(int ID)
        {
            string WardOfPatient = "";

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    WardOfPatient = AllPatientInfo[i].WardClass;
                }
            }

            return WardOfPatient;
        }

        public List<string> GetPatientMedicineList(int ID)
        {
            var MedList = new List<string>();

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    MedList = AllPatientInfo[i].ListOfMedicines;
                }
            }

            return MedList;
        }

        //Calculate total amount to be paid by patient

        public double GetDepartmentPrice(int ID)
        {
            double price = 0;
            for(int i = 0; i < AllPatientInfo.Count; i++)
            {
                if(AllPatientInfo[i].PatientID == ID)
                {
                    if(AllPatientInfo[i].Department == "Outpatient")
                    {
                        price = 30.00;
                    }
                    else if (AllPatientInfo[i].Department == "Critical Care")
                    {
                        price = 300.00;
                    }
                    else if (AllPatientInfo[i].Department == "A&E")
                    {
                        price = 150.00;
                    }
                    else if (AllPatientInfo[i].Department == "Therapy")
                    {
                        price = 80.00;
                    }
                }
            }

            return price;
        }
            
        public double GetWardPrice(int ID)
        {
            double price = 0;
            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    if (AllPatientInfo[i].WardClass == "Class A")
                    {
                        price = 180.00;
                    }
                    else if (AllPatientInfo[i].WardClass == "Class B")
                    {
                        price = 150.00;
                    }
                    else if (AllPatientInfo[i].WardClass == "Class C")
                    {
                        price = 120.00;
                    }
                    else if (AllPatientInfo[i].WardClass == "Private")
                    {
                        price = 250.00;
                    }
                }
            }

            return price;
        }

        public double GetMedicinePrice(int ID)
        {
            double price = 0;
            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    for (int j = 0; j < AllPatientInfo[i].ListOfMedicines.Count; j++)
                    {
                        if (AllPatientInfo[i].ListOfMedicines[j] == "Paracetamol")
                        {
                            price += 5.85;
                        }
                        else if (AllPatientInfo[i].ListOfMedicines[j] == "Antibiotics")
                        {
                            price += 12.40;
                        }
                        else if (AllPatientInfo[i].ListOfMedicines[j] == "AntiInflammation")
                        {
                            price += 6.75;
                        }
                        else if (AllPatientInfo[i].ListOfMedicines[j] == "Antihistamines")
                        {
                            price += 15.90;
                        }
                        else if (AllPatientInfo[i].ListOfMedicines[j] == "Antacids")
                        {
                            price += 4.35;
                        }
                        else if (AllPatientInfo[i].ListOfMedicines[j] == "Painkillers")
                        {
                            price += 8.50;
                        }
                    }             
                }
            }

            return price;
        }
        public double CalculateTotalBill(int ID)
        { 
            Console.WriteLine("How many nights have the patient stayed in the ward?");
            Console.Write("Answer here: ");
            int nights = Convert.ToInt32(Console.ReadLine());

            double TotalBill = 0;
            double FinalBill = 0;

            double DeptPrice = GetDepartmentPrice(ID);
            double WardPrice = (GetWardPrice(ID) * nights);
            double MedPrice = GetMedicinePrice(ID);

            TotalBill = DeptPrice + WardPrice + MedPrice;
            double GST = TotalBill * 0.07;

            Console.Write("Enter Subsidy amount here (%): ");
            double sAmount = Convert.ToDouble(Console.ReadLine());
            double Subsidised = 0;
            if (sAmount != 0)
            {
                Subsidised = TotalBill * (sAmount / 100);
            }

            FinalBill = (TotalBill + GST) - Subsidised;
            return FinalBill;
        }

        public void SettleBill(int ID)
        {
            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    if (AllPatientInfo[i].BillPayment == true)
                    {
                        Console.WriteLine("You have already paid the bill.");
                        Console.WriteLine("Please wait to be discharged by the doctor in charge.\n");
                    }
                    else
                    {
                        for (int j = 0; j < AllBills.Count; j++)
                        {
                            if (AllBills[j].PatientID == ID)
                            {
                                AllBills[j].Status = "Paid";
                                AllPatientInfo[i].BillPayment = true;

                                Console.WriteLine();
                                Console.WriteLine("Thank you for payng the bill!");
                                Console.WriteLine("Please wait awhile for the payment to go through.");
                                Console.WriteLine("You will be officially discharged when the doctor says so.\n");
                            }
                            else if(j == AllBills.Count-1 && AllBills[j].PatientID != ID)
                            {
                                Console.WriteLine("There are no bills to be paid at the moment.\n");
                            }
                        }
                        
                    }
                }
            }

            UpdateBillJsonFile();
            UpdatePatientJsonFile();
        }

        //Creating new bills
        public void GenerateBill(int ID)
        {
            bool CreateBool = false;
            for(int i = 0; i < AllBills.Count; i++)
            {
                if(AllBills[i].PatientID == ID)
                {
                    Console.WriteLine($"There is already a bill created for {AllBills[i].PatientName}\n");
                    CreateBool = false;
                }
                else
                    CreateBool = true;
            }

            if (CreateBool == true)
            {
                AllBills = JsonConvert.DeserializeObject<List<Bill>>(File.ReadAllText("Bills.Json"));
                string BList = JsonConvert.SerializeObject(AllBills);
                File.WriteAllText("Bills.Json", BList);

                Bill newBill = new Bill();
                newBill = newBill.CreateNewBill(ID);

                AllBills.Add(newBill);
                UpdateBillJsonFile();

                Console.WriteLine($"A new bill no. {newBill.BillID} has been credited to patient {newBill.PatientName}");
                Console.WriteLine("Please pay the bill before you are able to get discharged.\n");
            }
            else
                Console.WriteLine("Unable to crate a new bill right now.\n");
        }

        public void ShowTheBill(int ID)
        {
            for (int i = 0; i < AllBills.Count; i++)
            {
                if(AllBills[i].PatientID == ID)
                {
                    Console.WriteLine($"Bill ID: {AllBills[i].BillID}");

                    Console.WriteLine($"Patient ID: {AllBills[i].PatientID}");
                    Console.WriteLine($"Name: {AllBills[i].PatientName}");

                    Console.WriteLine($"Department: {AllBills[i].Department}");
                    Console.WriteLine($"Ward: {AllBills[i].WardClass}\n");

                    for (int j = 0; j < AllBills[i].ListOfMedicine.Count; j++)
                    {
                        Console.WriteLine($"Medicine {j}: {AllBills[i].ListOfMedicine[j]}");
                    }

                    Console.WriteLine();
                    Console.WriteLine($"GST Amount: {AllBills[i].GST}");
                    Console.WriteLine($"Subsidised Amount: {AllBills[i].Subsidy}");
                    Console.WriteLine($"Total Amount Payable: {AllBills[i].Total}");
                    Console.WriteLine($"Payment Status: {AllBills[i].Status}\n");
                }
                else if (AllBills[i].PatientID != ID && i == AllBills.Count-1)
                {
                    Console.WriteLine("There are currently no bills to be shown.\n");
                }
            }
        }
    }
}
