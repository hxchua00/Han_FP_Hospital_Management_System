using HospitalManagement.Common.Common;

namespace HospitalManagement.Common.DTO
{
    public class BillDTO
    {
        public int BillID { get; set; }                    
        public double GST { get; set; }                     
        public double Subsidy { get; set; }                
        public BillStatusEnum Status { get; set; }                  
        public double TotalAmount { get; set; }
        public BillDTO(int totalBillCounter, double gst, double subsidy, double totalBill, BillStatusEnum status)
        {
            BillID = totalBillCounter + 10000;
            GST = gst;
            Subsidy = subsidy;
            TotalAmount = totalBill;
            Status = status;
        }
    }
}
