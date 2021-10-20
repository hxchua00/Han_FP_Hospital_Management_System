using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace Han_FP_Hospital_Management_System
{
    class Hospital_Worker
    {
        public Guid WorkerID { get; private set; }
        public string HashedPassword { get; private set; }

        List<Patient> AllPatientInfo = new List<Patient>();                             //Stores all pateint information
        Dictionary<int, Guid> QueueList = new Dictionary<int, Guid>();                  //Reference to find Patient info easier, KEY: ID, VALUE: PatientID
        Dictionary<Guid, string> Patient_Department = new Dictionary<Guid, string>();   //KEY: PatientID, VALUE: DeptName

        int patientRefNum = 1;                                                          //Reference Number for patients

        public Hospital_Worker() { }

        public Hospital_Worker(Guid ID, string pwd)
        {
            WorkerID = ID;
            HashedPassword = pwd;
        }
        //Add new patient to database
        public void AddPatient(Patient newPatient)
        {
            //Add patient info to list
            AllPatientInfo.Add(newPatient.Registration());

            //Adding patient GUID ID to the reference Dictionary
            //When getting ID, the Key will be be returned and used to show the GUID ID instead of returnining the actual GUID ID
            QueueList[patientRefNum] = newPatient.PatientID;
            patientRefNum++;

            newPatient.NumOfVisits++;

            //Write Patient details to JSON File and store data there
            string CreateFileJson = JsonConvert.SerializeObject(AllPatientInfo);
            File.WriteAllText("Patient_Information.Json", CreateFileJson);

            Console.WriteLine("Thank you. Your registration has finished.");
            foreach(KeyValuePair<int,Guid> patientRef in QueueList)
            {
                if(newPatient.PatientID == patientRef.Value)
                {
                    Console.WriteLine($"Your number will be {patientRef.Key}\n");
                }
            }
        }

        //After seeing the doctor, Patient will be assigned to the department related to their illness
        public void AdmitPatient()
        {

        }
        
        //Remove Patient from the QueueList and Department, but data still remain for future reference
        public void DischargePatient()
        {

        }

        public void ViewPatientInfo()
        {
            List<Patient> WritePatientInfo = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Patient_Information.Json"));

            for(int i = 0; i< WritePatientInfo.Count; i++)
            {
                Console.WriteLine($"Patient's ID: {WritePatientInfo[i].PatientID}");
                Console.WriteLine($"Patient's Name: {WritePatientInfo[i].PatientName}");
                Console.WriteLine();
                Console.WriteLine($"Patient's Ethnicity: {WritePatientInfo[i].Ethnicity}");
                Console.WriteLine($"Patient's Address: {WritePatientInfo[i].Address}");
                Console.WriteLine($"Patient's Contact No.: {WritePatientInfo[i].ContactNum}");
                Console.WriteLine();
                Console.WriteLine($"Patient's Weight: {WritePatientInfo[i].DoctorInCharge}");
                Console.WriteLine($"Patient's Height: {WritePatientInfo[i].NumOfVisits}");
                Console.WriteLine();
                Console.WriteLine($"Reason for visit: \n{WritePatientInfo[i].ReasonForVisit}");
            }
        }

        public void CalculateTotalBill()
        {

        }

        public void GenerateBill()
        {

        }


    }
}
