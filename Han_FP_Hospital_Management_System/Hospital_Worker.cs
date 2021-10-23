using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace Han_FP_Hospital_Management_System
{
    class Hospital_Worker : StaffAccounts   
    {
        public static List<Patient> AllPatientInfo = new List<Patient>();                      //Stores all pateint information
        public static List<Bill> AllBills = new List<Bill>();                                  //Stores all bills

        public static  Dictionary<int, List<string>> IllnessRecord;          //Keeps a record of all illnees the patient came to the hospital for
        
        static Hospital_Worker() 
        {
            var patientList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));
            AllPatientInfo.AddRange(patientList);

            //var billList = JsonConvert.DeserializeObject<List<Bill>>(File.ReadAllText("Bills.Json"));
            //AllBills.AddRange(billList);

            IllnessRecord = new Dictionary<int, List<string>>();
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

            List<Patient> PatientList = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

            string pList = JsonConvert.SerializeObject(PatientList);
            File.WriteAllText("Patient_Information.Json", pList);

        }

        //Add new patient to database
        public void AddPatient()
        {
            AllPatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

            string pList = JsonConvert.SerializeObject(AllPatientInfo);
            File.WriteAllText("Patient_Information.Json", pList);

            Patient newPatient = new Patient();

            newPatient = newPatient.Registration();
            //Add patient info to list
            AllPatientInfo.Add(newPatient);
 
            UpdatePatientJsonFile();
        }

        //Admit the patient to the respective department
        //Inclusive of consultation of doctors.
        public void AdmitPatient(int ID)
        {
            Console.WriteLine("What seems to be the problem?\n");
            Console.WriteLine("1) Normal Cases(Flu, Cough, Fever, etc");
            Console.WriteLine("2) Serious Cases (Allergy reaction, Accidents, Emergency)");
            Console.WriteLine("3) Suspect Internal Injury\n");

            int issues = Convert.ToInt32(Console.ReadLine());
            string Dept = "";
            string status = "";
            if(issues == 1)
            {
                Dept = "OPD";
                status = "Normal case";
            }
            else if (issues == 2)
            {
                Dept = "IP";
                status = "Seruous case";
            }
            else if(issues == 3)
            {
                Dept = "Radiology";
                status = "Suspect Internal Injury";
            }
 

            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if(AllPatientInfo[i].PatientID == ID)
                {
                    AllPatientInfo[i].Department = Dept;
                    //AllPatientInfo[i].Status = status;

                    //Adds the illness to the record
                    //IllnessRecord[AllPatientInfo[i].PatientID] = status;
                }  
            }

            UpdatePatientJsonFile();

        }
        
        //Remove Patient from the List, but data still remain for future reference
        public void DischargePatient(int ID)
        {
            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    AllPatientInfo[i].Department = null;
                    //AllPatientInfo[i].Status = null;
                }
            }

            Console.WriteLine($"Patient {ID} has been dischaarged.\n");
            UpdatePatientJsonFile();
        }

        //Search through database for Patient's Information via Reference ID given to them
        public void ViewPatientInfo(int ID)
        {
            List<Patient> WritePatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

            for (int i = 0; i < WritePatientInfo.Count; i++)
            {
                if (WritePatientInfo[i].PatientID == ID)
                {
                    Console.WriteLine($"Patient's ID: {WritePatientInfo[i].PatientID}");
                    Console.WriteLine($"Patient's Name: {WritePatientInfo[i].PatientName}\n");
                    Console.WriteLine($"Age: {WritePatientInfo[i].PatientAge}");
                    Console.WriteLine($"Weight: {WritePatientInfo[i].PatientWeight}");
                    Console.WriteLine($"Height: {WritePatientInfo[i].PatientHeight}\n");
                    Console.WriteLine($"Ethnicity: {WritePatientInfo[i].Ethnicity}");
                    Console.WriteLine($"Address: {WritePatientInfo[i].Address}");
                    Console.WriteLine($"Contact Number: {WritePatientInfo[i].ContactNum}\n");
                    Console.WriteLine($"Doctor In-charge: {WritePatientInfo[i].DoctorInCharge}");
                    Console.WriteLine($"Department: {WritePatientInfo[i].Department}");
                    //Console.WriteLine($"Status: {WritePatientInfo[i].Status}");
                }
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

        //Calculate total amount to be paid by patient
        public double CalculateTotalBill(int ID)
        {
            Bill b = new Bill();
            double medprice =0, deptprice=0, wardprice=0, totalbill=0;

            Console.WriteLine("Name of medicine: ");
            string meds = Console.ReadLine();
            Console.WriteLine("How many night are you warded?");
            int nights = Convert.ToInt32(Console.ReadLine());

            foreach (KeyValuePair<string, double> medicine in b.MedicinePrice)
            {
                if (medicine.Key == meds)
                {
                    medprice = medicine.Value;
                }
            }

            foreach (KeyValuePair<string, double> dept in b.DepartmentPrice)
            {
                foreach(Bill bill in AllBills)
                {
                    if(bill.BillID == ID)
                    {
                        deptprice = dept.Value;
                    }
                }
            }

            foreach (KeyValuePair<string, double> ward in b.WardPrice)
            {
                foreach (Bill bill in AllBills)
                {
                    if (bill.BillID == ID)
                    {
                        if(bill.WardClass == ward.Key)
                        {
                            wardprice = ward.Value * nights;
                        }
                    }
                }
            }

            double priceBeforeTax = medprice + deptprice + wardprice;

            foreach (Bill bill in AllBills)
            {
                if (bill.BillID == ID)
                {
                    totalbill = priceBeforeTax + (priceBeforeTax * bill.GST) + (priceBeforeTax * bill.Subsidy);
                }
            }
            

            return totalbill;
        }

        public void SettleBill(int ID)
        {
            //Remove Patient Info from neccessary areas
            Console.WriteLine("Thank you for coming! Stay safe and healthy!");
            Environment.Exit(0);
        }

        //Creating new bills
        public void GenerateBill(int ID)
        {
            Hospital_Worker HW = new Hospital_Worker();
            int BillNum = 0;
            string Dept = "";

            AllBills = JsonConvert.DeserializeObject<List<Bill>>(File.ReadAllText("Bills.Json"));

            string bList = JsonConvert.SerializeObject(AllBills);
            File.WriteAllText("Bills.Json", bList);

            Console.WriteLine("Any subsidy plans? if yes, how much?");
            Console.Write("Enter Subsidy amount here (%): ");
            double Subsidy = Convert.ToDouble(Console.ReadLine());


            for (int i = 0; i < AllPatientInfo.Count; i++)
            {
                if (AllPatientInfo[i].PatientID == ID)
                {
                    Dept = AllPatientInfo[i].Department;
                    //AllPatientInfo[i].Status = null;
                }
            }

            Bill newBill = new Bill()
            {
                BillID = BillNum,
                PatientID = ID,
                PatientName = HW.GetPatientName(ID),
                Department = Dept,
                WardClass = null,
                GST = 0.07,
                Subsidy = Subsidy/100,
                Total = 0,
            };

            AllBills.Add(newBill);
            UpdateBillJsonFile();
        }
    }
}
