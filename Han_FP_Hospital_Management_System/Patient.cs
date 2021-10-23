using System;
using System.Collections.Generic;

namespace Han_FP_Hospital_Management_System
{
    class Patient
    {
        public static int patientcounter = 1000;
        public int PatientID { get; set; }             //Unique ID for patients
		public string PatientName { get; set; }         //Name of Patient
		public double PatientWeight { get; set; }       //Weight of Patient
		public double PatientHeight { get; set; }       //Height of Patient
        public int PatientAge { get; set; }             //Age of Patient
		public string Ethnicity { get; set; }           //Race of the Patient
		public string Address { get; set; }             //Patient's Current Staying Address 
		public int ContactNum { get; set; }             //Patient's contact number
		public string Department { get; set; }          //Department the Patient is admitted to

        public string WardClass { get; set; }           // Ward that the patient admitted to
        public List<string> listOfIllness{ get; set; }              //Reason the Patient is visiting the hospital for

		public string DoctorInCharge;       //Name of Doctor In Charge
		public int NumOfVisits;             //Total number of visits the patient made to the hospital, AND see the doctor

        string[] Doctors = new string[] { "Dr. Amakatsu", "Dr. Kelvin", "Dr. Jane", "Dr. Kim Hon Lee", "Dr. Restaya" };
        Random rand = new Random();

        public Patient() { }

		public Patient Registration()
        {
            Console.WriteLine("Enter patient's Name: ");
            string pName = Console.ReadLine();
            Console.WriteLine("Enter patient's Age: ");
            int pAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter patient's Weight (in KG): ");
            double pWeight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter patient's Height (in CM): ");
            double pHeight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter patient's Ethnicity: ");
            string pEthnicity = Console.ReadLine();
            Console.WriteLine("Enter patient's Address: ");
            string pAddress = Console.ReadLine();
            Console.WriteLine("Enter patient's Contact Number: ");
            int pContactNum = Convert.ToInt32(Console.ReadLine());

            var lst = new List<string>();
            while (true)
            {
                Console.WriteLine("Enter patient's Illness: ");
                string illness = Console.ReadLine();
                lst.Add(illness);

                Console.WriteLine("Do you want to add more Illness: ");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No ");
                int res = Int32.Parse(Console.ReadLine());

                if(res == 2)
                {
                    break;
                }
            }
            


            Console.WriteLine();

            int DocInCharge = rand.Next(0, 5);

            Patient newPatient = new Patient()
            {
                PatientID       = patientcounter,
                PatientName     = pName,

                PatientAge      = pAge,
                PatientWeight   = pWeight,
                PatientHeight   = pHeight,
                
                Ethnicity       = pEthnicity,
                Address         = pAddress,
                ContactNum      = pContactNum,

                listOfIllness = lst,
                DoctorInCharge  = Doctors[DocInCharge],
                Department      = null,
                WardClass       = null,
            };
            patientcounter++;
            return newPatient;
        }
	}
}
