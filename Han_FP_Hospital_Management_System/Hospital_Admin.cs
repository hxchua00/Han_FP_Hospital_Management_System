using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace Han_FP_Hospital_Management_System
{
    class Hospital_Admin : StaffAccounts
    {
        Hospital_Worker HW;
        public Hospital_Admin()
        {
            HW = new Hospital_Worker();
        }

        //Prints all patients in each department
        public void PrintPatientDept()
        {
            Console.WriteLine("All Patient in OutPatient (OPD): \n");
            for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
            {
                if (Hospital_Worker.AllPatientInfo[i].Department == "OPD")
                {
                    HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                }
            }

            Console.WriteLine("All Patient in InPatient (IP): \n");
            for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
            {
                if (Hospital_Worker.AllPatientInfo[i].Department == "IP")
                {
                    HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                }
            }

            Console.WriteLine("All Patient in Radiology: \n");
            for (int i = 0; i < Hospital_Worker.AllPatientInfo.Count; i++)
            {
                if (Hospital_Worker.AllPatientInfo[i].Department == "Radiology")
                {
                    HW.ViewPatientInfo(Hospital_Worker.AllPatientInfo[i].PatientID);
                }
            }
        }

        //Prints total number of bills geneerated
        public void PrintAllBills()
        {           
            Hospital_Worker.AllBills = JsonConvert.DeserializeObject<List<Bill>>(File.ReadAllText("Bills.Json"));

            foreach (Bill b in Hospital_Worker.AllBills)
            {
                Console.WriteLine($"Bill ID: {b.BillID}");
                
                Console.WriteLine($"Patient ID: {b.PatientID}");
                Console.WriteLine($"Name: {b.PatientName}");

                Console.WriteLine($"Department: {b.Department}");
                Console.WriteLine($"Ward: {b.WardClass}");
                Console.WriteLine($"GST Amount: {b.GST}");
                Console.WriteLine($"Subsidised Amount: {b.Subsidy}");

                Console.WriteLine($"Total Amount Payable: {b.Total}\n");

            }
        }
    }
}
