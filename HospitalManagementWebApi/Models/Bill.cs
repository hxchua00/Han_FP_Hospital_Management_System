using HospitalManagement.Common.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementWebApi.Models
{
    [Table("Bills")]
    public class Bill
    {
        [Key]
        public int BillID { get; set; }                     //Unique Identity for each bill generated
        public double GST { get; set; }                     //Government Service Tax
        public BillStatusEnum Status { get; set; }                  //Determines if this bill has been paid or not
        public double TotalAmount { get; set; }           
        public Bill() { }
        public Bill(int totalBillCounter, double gst, double totalBill, BillStatusEnum status)
        {
            BillID = totalBillCounter + 10000;
            GST = gst;
            TotalAmount = totalBill;
            Status = status;
        }
    }
}
