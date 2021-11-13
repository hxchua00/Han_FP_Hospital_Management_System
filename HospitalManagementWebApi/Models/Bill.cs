using HospitalManagement.Common.Common;

namespace HospitalManagementWebApi.Models
{
    public class Bill
    {
        public int BillID { get; set; }                     //Unique Identity for each bill generated
        public double GST { get; set; }                     //Government Service Tax
        public double Subsidy { get; set; }                 //Subsidies for the bill, if any. Default is none.
        public BillStatusEnum Status { get; set; }                  //Determines if this bill has been paid or not
        public double TotalAmount { get; set; }           
        public Bill() { }
        public Bill(int totalBillCounter, double gst, double subsidy, double totalBill, BillStatusEnum status)
        {
            BillID = totalBillCounter + 10000;
            GST = gst;
            Subsidy = subsidy;
            TotalAmount = totalBill;
            Status = status;
        }
    }
}
