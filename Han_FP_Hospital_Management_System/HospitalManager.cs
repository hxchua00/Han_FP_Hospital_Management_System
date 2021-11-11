using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Newtonsoft.Json;
using System.Linq;

namespace Han_FP_Hospital_Management_System
{
    class HospitalManager : IHospitalManager
    {
        private List<Patient> AllPatientInfo = new List<Patient>();                                 //Stores all pateint information
        private Dictionary<int, List<string>> SymptomsRecord = new Dictionary<int, List<string>>();  //Stores records of all symptoms
        private IConfigurationManager _config;

        public HospitalManager(IConfigurationManager configuration)
        {
            _config = configuration;
        }
        public void Initialize()
        {
            if (!File.Exists("Patient_Information.Json"))
            {
                string CreatePatientJson = JsonConvert.SerializeObject(AllPatientInfo);
                File.WriteAllText("Patient_Information.Json", CreatePatientJson);
            }
            var patientList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));
            AllPatientInfo.AddRange(patientList);
        }

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
        public void AddPatient(Patient p)
        {
            try
            {
                AllPatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

                string pList = JsonConvert.SerializeObject(AllPatientInfo);
                File.WriteAllText("Patient_Information.Json", pList);

                //Add patient info to list
                if (p != null)
                {
                    AllPatientInfo.Add(p);

                    Console.WriteLine($"Name: {p.PatientName}, ID: {p.PatientID} has been added successfully!\n");
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
        public void AdmitPatient(int ID, PatientVisitRecord visitRecord)
        {
            try
            {
                Patient patientToAdmit = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
                patientToAdmit.VisitHistory.Add(visitRecord);
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
                Patient TargetPatient = WritePatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();

                Console.WriteLine($"Patient's ID: {TargetPatient.PatientID}");
                Console.WriteLine($"Name: {TargetPatient.PatientName}\n");
                Console.WriteLine($"Gender: {TargetPatient.PatientGender}");
                Console.WriteLine($"Age: {TargetPatient.PatientAge}");
                Console.WriteLine($"Ethnicity: {TargetPatient.Ethnicity}"); 
                Console.WriteLine($"Weight: {TargetPatient.PatientWeight}");
                Console.WriteLine($"Height: {TargetPatient.PatientHeight}\n");

                Console.WriteLine($"Address: {TargetPatient.Address}");
                Console.WriteLine($"Contact Number: {TargetPatient.ContactNum}\n");

                for(int i = 0; i < TargetPatient.VisitHistory.Count; i++)
                {
                    Console.WriteLine($"Doctor in charge: {TargetPatient.VisitHistory[i].DoctorInCharge}");
                    Console.WriteLine($"Department: {TargetPatient.VisitHistory[i].Department}");
                    Console.WriteLine($"Ward: {TargetPatient.VisitHistory[i].Ward}");
                    Console.WriteLine($"Duration of ward stay: {TargetPatient.VisitHistory[i].StayDuration}\n");
                    for(int j = 0; j < TargetPatient.VisitHistory[i].ListOfSymptoms.Count; j++)
                    {
                        Console.WriteLine($"Recorded Symptoms: {TargetPatient.VisitHistory[i].ListOfSymptoms[j]}");
                    }
                    Console.WriteLine();
                    for (int j = 0; j < TargetPatient.VisitHistory[i].ListOfMedicines.Count; j++)
                    {
                        Console.WriteLine($"Recorded Symptoms: {TargetPatient.VisitHistory[i].ListOfMedicines[j]}");
                    }

                    Console.WriteLine($"Bill Information: {TargetPatient.VisitHistory[i].BillInformation}");
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

        public double CalculateTotalBill(int ID)
        {
            double TotalBill = 0;
            double FinalBill = 0;
            try
            {
                Console.WriteLine("How many nights have the patient stayed in the ward?");
                Console.Write("Answer here: ");
                Console.WriteLine();

                int nights = Convert.ToInt32(Console.ReadLine());

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

            return FinalBill;
        }

        public void SettleBill(int ID)
        {
            try
            {
                //for (int i = 0; i < AllPatientInfo.Count; i++)
                //{
                //    if (AllPatientInfo[i].PatientID == ID)
                //    {
                //        if (AllPatientInfo[i].BillPayment == true)
                //        {
                //            Console.WriteLine("You have already paid the bill.");
                //            Console.WriteLine("Please wait to be discharged by the doctor in charge.\n");
                //        }
                //        else
                //        {
                //            for (int j = 0; j < AllBills.Count; j++)
                //            {
                //                if (AllBills[j].PatientID == ID)
                //                {
                //                    AllBills[j].Status = "Paid";
                //                    AllPatientInfo[i].BillPayment = true;

                //                    Console.WriteLine();
                //                    Console.WriteLine("Thank you for payng the bill!");
                //                    Console.WriteLine("Please wait awhile for the payment to go through.");
                //                    Console.WriteLine("You will be officially discharged when the doctor says so.\n");
                //                }
                //                else if (j == AllBills.Count - 1 && AllBills[j].PatientID != ID)
                //                {
                //                    Console.WriteLine("There are no bills to be paid at the moment.\n");
                //                }
                //            }

                //        }
                //    }
                //}

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

        //Creating new bills
        public void GenerateBill(int ID, Bill BillInfo)
        {
            try
            {
                Patient AdmittedPatient = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
                
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

        public void ShowTheBill(int ID)
        {
            try
            {
                //for (int i = 0; i < AllBills.Count; i++)
                //{
                //    if (AllBills[i].PatientID == ID)
                //    {
                //        Console.WriteLine($"Bill ID: {AllBills[i].BillID}");

                //        Console.WriteLine($"Patient ID: {AllBills[i].PatientID}");
                //        Console.WriteLine($"Name: {AllBills[i].PatientName}");

                //        Console.WriteLine($"Department: {AllBills[i].Department}");
                //        Console.WriteLine($"Ward: {AllBills[i].WardClass}\n");

                //        for (int j = 0; j < AllBills[i].ListOfMedicine.Count; j++)
                //        {
                //            Console.WriteLine($"Medicine {j}: {AllBills[i].ListOfMedicine[j]}");
                //        }

                //        Console.WriteLine();
                //        Console.WriteLine($"GST Amount: {AllBills[i].GST}");
                //        Console.WriteLine($"Subsidised Amount: {AllBills[i].Subsidy}");
                //        Console.WriteLine($"Total Amount Payable: {AllBills[i].Total}");
                //        Console.WriteLine($"Payment Status: {AllBills[i].Status}\n");
                //    }
                //    else if (AllBills[i].PatientID != ID && i == AllBills.Count - 1)
                //    {
                //        Console.WriteLine("There are currently no bills to be shown.\n");
                //    }
                //}
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

        public string GetPatientName(int ID)
        {
            //return NameOfPatient;
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().PatientName;

        }
        //Returns Department the patient is admitted to
        public DepartmentEnum GetPatientDept(int ID)
        {
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.FirstOrDefault().Department;
        }
        //Returns Ward the patient is admitted to
        public WardEnum GetPatientWard(int ID)
        {
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.FirstOrDefault().Ward;
        }
        //Returns list of medicines from VisitHistory
        public List<string> GetPatientMedicineList(int ID)
        {
            return AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault().VisitHistory.FirstOrDefault().ListOfMedicines;
        }

        //Calculate total amount to be paid by patient
        public double GetDepartmentPrice(int ID)
        {
            double price = 0;
            if (GetPatientDept(ID) == DepartmentEnum.Outpatient)
            {
                price = 30.00;
            }
            else if (GetPatientDept(ID) == DepartmentEnum.Inpatient)
            {
                price = 100.00;
            }
            else if (GetPatientDept(ID) == DepartmentEnum.Emergency)
            {
                price = 250.00;
            }

            return price;
        }

        public double GetWardPrice(int ID)
        {
            double price = 0;
            if (GetPatientWard(ID) == WardEnum.ClassA)
            {
                price = 250.00;
            }
            else if (GetPatientWard(ID) == WardEnum.ClassB)
            {
                price = 150.00;
            }
            else if (GetPatientWard(ID) == WardEnum.ClassC)
            {
                price = 100.00;
            }
            else if (GetPatientWard(ID) == WardEnum.Private)
            {
                price = 500.00;
            }

            return price;
        }

        public double GetMedicinePrice(int ID)
        {
            double price = 0;
            for (int i = 0; i < GetPatientMedicineList(ID).Count; i++)
            {
                if (GetPatientMedicineList(ID)[i] == "Paracetamol")
                {
                    price += 8.85;
                }
                else if (GetPatientMedicineList(ID)[i] == "Antibiotics")
                {
                    price += 15.40;
                }
                else if (GetPatientMedicineList(ID)[i] == "AntiInflammation")
                {
                    price += 10.75;
                }
                else if (GetPatientMedicineList(ID)[i] == "Antihistamine")
                {
                    price += 20.90;
                }
                else if (GetPatientMedicineList(ID)[i] == "Lozenges")
                {
                    price += 6.35;
                }
                else if (GetPatientMedicineList(ID)[i] == "Painkillers")
                {
                    price += 8.50;
                }
            }

            return price;
        }

        public bool ValidatePatient(int ID)
        {
            Patient pObj = AllPatientInfo.Where(x => x.PatientID == ID).FirstOrDefault();
            if (pObj != null)
                return true;
            else
                return false;
        }

        public void PrintAllDepartments()
        {
            try
            {
                Patient outpatients = AllPatientInfo.Where(x => x.VisitHistory.FirstOrDefault().Department == DepartmentEnum.Outpatient).FirstOrDefault();
                Patient inpatients = AllPatientInfo.Where(x => x.VisitHistory.FirstOrDefault().Department == DepartmentEnum.Inpatient).FirstOrDefault();
                Patient emergencyPatients = AllPatientInfo.Where(x => x.VisitHistory.FirstOrDefault().Department == DepartmentEnum.Emergency).FirstOrDefault();

                //Outpatient Department
                Console.WriteLine("==================================================");
                Console.WriteLine("Patients in 'OutPatient' department....\n");
                for(int i = 0; i < AllPatientInfo.Count; i++)
                {
                    if(AllPatientInfo[i] == outpatients)
                    {
                        ViewPatientInfo(outpatients.PatientID);
                    }
                }
                Console.WriteLine();
                //Inpatient Department
                Console.WriteLine("Patients in 'Inpatient' department....\n");
                for (int i = 0; i < AllPatientInfo.Count; i++)
                {
                    if (AllPatientInfo[i] == inpatients)
                    {
                        ViewPatientInfo(inpatients.PatientID);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Patients in 'Emergency' department....\n");
                for (int i = 0; i < AllPatientInfo.Count; i++)
                {
                    if (AllPatientInfo[i] == emergencyPatients)
                    {
                        ViewPatientInfo(emergencyPatients.PatientID);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("==================================================");
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
        public void PrintAllBills()
        {
            try
            {
                for (int i = 0; i < AllPatientInfo.Count; i++)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Bill ID: {AllPatientInfo[i].VisitHistory[i].BillInformation.BillID}");
                    Console.WriteLine($"GST amount: {AllPatientInfo[i].VisitHistory[i].BillInformation.GST}");
                    Console.WriteLine($"Subsidies: {AllPatientInfo[i].VisitHistory[i].BillInformation.Subsidy}");
                    Console.WriteLine($"Total Amount Payable: {AllPatientInfo[i].VisitHistory[i].BillInformation.TotalAmount}");
                    Console.WriteLine($"Status: {AllPatientInfo[i].VisitHistory[i].BillInformation.Status}\n");
                    Console.WriteLine("==================================================");
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
    }
}
