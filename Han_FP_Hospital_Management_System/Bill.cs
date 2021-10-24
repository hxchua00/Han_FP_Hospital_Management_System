using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System
{
    class Bill
    {
        public int BillID { get; set; }                     //Unique Identity for each bill generated
        public int PatientID { get; set; }                  //Patient's ID
        public string PatientName { get; set; }             //Patient's Name
        public string Department { get; set; }              //Department name
        public string WardClass { get; set; }               //Class of ward

        public List<string> ListOfMedicine { get; set; }    //List of provided medicine
        public double GST { get; set; }                     //Government Service Tax
        public double Subsidy { get; set; }                 //Subsidies for the bill, if any. Default is none.

        public string Status { get; set; }                  //Determines if this bill has been paid or not
    
        public double Total { get; set; }           

        public Dictionary<string, double> MedicinePrice = new Dictionary<string, double>();    //Price of medicine
        public Dictionary<string, double> DepartmentPrice = new Dictionary<string, double>();  //Price of each department's consultant 
        public Dictionary<string, double> WardPrice = new Dictionary<string, double>();        //Price of each different class wards, per night

        public static int BillNum = 1;
        Hospital_Worker HW;

        public Bill()
        {
            HW = new Hospital_Worker();
        }

        public Bill CreateNewBill(int ID)
        {
            Console.Write("Enter Subsidy amount here (%): ");
            double Subsidy = Convert.ToDouble(Console.ReadLine());

            Bill newBill = new Bill()
            {
                BillID = BillNum,
                PatientID = ID,
                PatientName = HW.GetPatientName(ID),
                Department = HW.GetPatientDept(ID),
                WardClass = HW.GetPatientWard(ID),

                ListOfMedicine = HW.GetPatientMedicineList(ID),

                GST = 0.07,
                Subsidy = Subsidy / 100,
                Total = HW.CalculateTotalBill(ID),
                Status = "Not Paid"
            };
            BillNum++;
            return newBill;
        }
    }
}
