using HospitalManagement.Common.Common;

namespace HospitalManagement.Common.DTO
{
    public class BillDTO
    {
        public int BillID { get; set; }                    
        public double GST { get; set; }                                 
        public BillStatusEnum Status { get; set; }                  
        public double TotalAmount { get; set; }

        public BillDTO() { }
        public BillDTO(int totalBillCounter, double gst, double totalBill, BillStatusEnum status)
        {
            BillID = totalBillCounter + 10000;
            GST = gst;
            TotalAmount = totalBill;
            Status = status;
        }
    }
}
