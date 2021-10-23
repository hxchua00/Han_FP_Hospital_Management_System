using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han_FP_Hospital_Management_System
{
    class Bill
    {
        public int BillID { get; set; }             //Unique Identity for each bill generated
        public int PatientID { get; set; }          //Patient's ID
        public string PatientName { get; set; }     //Patient's Name
        public string Department { get; set; }      //Department name
        public string WardClass { get; set; }       //Class of ward
        public double GST { get; set; }             //Government Service Tax
        public double Subsidy { get; set; }         //Subsidies for the bill, if any. Default is none.

        public double Total { get; set; }

        public Dictionary<string, double> MedicinePrice = new Dictionary<string, double>();    //Price of medicine
        public Dictionary<string, double> DepartmentPrice = new Dictionary<string, double>();  //Price of each department's consultant 
        public Dictionary<string, double> WardPrice = new Dictionary<string, double>();        //Price of each different class wards, per night

        static Bill()
        {
            Bill b = new Bill();
            b.MedicinePrice.Add("Paracetamol", 3.0);
            b.MedicinePrice.Add("Cough Drops", 1.5);
            b.MedicinePrice.Add("Diarrhea Medicine", 2.0);

            b.DepartmentPrice.Add("OPD", 15);
            b.DepartmentPrice.Add("IP", 100);
            b.DepartmentPrice.Add("Radiology", 20);

            b.WardPrice.Add("Class A", 80);
            b.WardPrice.Add("Class B", 60);
            b.WardPrice.Add("Class C", 40);
        }

        public Bill()
        {

        }
    }
}
