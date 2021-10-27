using System.Collections.Generic;

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

        public static int BillNum = 1;
        Hospital_Worker HW;

        public Bill()
        {
            HW = new Hospital_Worker();
        }

        public Bill CreateNewBill(int ID)
        {
            Bill newBill = new Bill()
            {
                BillID = BillNum + Hospital_Worker.AllBills.Count,
                PatientID = ID,
                PatientName = HW.GetPatientName(ID),
                Department = HW.GetPatientDept(ID),
                WardClass = HW.GetPatientWard(ID),

                ListOfMedicine = HW.GetPatientMedicineList(ID),

                GST = 0.07,
                Subsidy = 0,
                Total = HW.CalculateTotalBill(ID),
                Status = "Not Paid"
            };
            BillNum++;
            return newBill;
        }
    }
}
