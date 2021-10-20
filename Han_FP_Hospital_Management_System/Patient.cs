using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System
{
    class Patient
    {
        public Guid PatientID { get; set; }             //Unique ID for patients
		public string PatientName { get; set; }         //Name of Patient
		public double PatientWeight { get; set; }       //Weight of Patient
		public double PatientHeight { get; set; }       //Height of Patient
		public string Ethnicity { get; set; }           //Race of the Patient
		public string Address { get; set; }             //Patient's Current Staying Address 
		public int ContactNum { get; set; }              //Patient's contact number
		public string ReasonForVisit { get; set; }      //Reason of visit for Patient

		public string DoctorInCharge;       //Name of Doctor In Charge
		public int NumOfVisits;             //Total number of visits the patient made to the hospital, AND see the doctor

        string[] Doctors = new string[] { "Dr. Amakatsu", "Dr. Kelvin", "Dr. Jane", "Dr. Kim Hon Lee", "Dr. Restaya" };
        Random rand = new Random();

        public Patient() { }

		public Patient Registration()
        {
            Console.WriteLine("Enter patient's Name: ");
            string pName = Console.ReadLine();
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
            
            Console.WriteLine();

            int DocInCharge = rand.Next(0, 5);

            Patient newPatient = new Patient()
            {
                PatientID = Guid.NewGuid(),
                PatientName = pName,
                Ethnicity = pEthnicity,

                PatientWeight = pWeight,
                PatientHeight = pHeight,

                ContactNum = pContactNum,
                Address = pAddress,

                DoctorInCharge = Doctors[DocInCharge],
            };

            return newPatient;
        }
	}
}
