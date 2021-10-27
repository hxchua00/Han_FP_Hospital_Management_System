using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Han_FP_Hospital_Management_System
{
    class Hospital_Admin
    {
        Hospital_Worker HW;
        public Hospital_Admin()
        {
            HW = new Hospital_Worker();
        }

        //Prints all patients in each department
        public void PrintPatientDept()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("All Patient in Outpatient: \n");
                for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
                {
                    if (Hospital_Worker.AllPatientInfo[i].Department == "Outpatient")
                    {
                        HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                    }
                }

                Console.WriteLine("All Patient in Critical Care: \n");
                for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
                {
                    if (Hospital_Worker.AllPatientInfo[i].Department == "Critical Care")
                    {
                        HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("All Patient in A&E: \n");
                for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
                {
                    if (Hospital_Worker.AllPatientInfo[i].Department == "A&E")
                    {
                        HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("All Patient in Therapy: \n");
                for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
                {
                    if (Hospital_Worker.AllPatientInfo[i].Department == "Therapy")
                    {
                        HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                    }
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

        //Prints total number of bills geneerated
        public void PrintAllBills()
        {           
            foreach (Bill b in Hospital_Worker.AllBills)
            {
                Console.WriteLine();
                Console.WriteLine("==============================\n");
                Console.WriteLine($"Bill ID: {b.BillID}");
                
                Console.WriteLine($"Patient ID: {b.PatientID}");
                Console.WriteLine($"Name: {b.PatientName}");

                Console.WriteLine($"Department: {b.Department}");
                Console.WriteLine($"Ward: {b.WardClass}\n");

                for(int i =0; i< b.ListOfMedicine.Count; i++)
                {
                    Console.WriteLine($"Medicine {i+1}: {b.ListOfMedicine[i]}");
                }

                Console.WriteLine();
                Console.WriteLine($"GST Amount: {b.GST}");
                Console.WriteLine($"Subsidised Amount: {b.Subsidy}");
                Console.WriteLine($"Total Amount Payable: {b.Total}");
                Console.WriteLine($"Payment Status: {b.Status}\n");
                Console.WriteLine("==============================");
            }
        }
    }
}
