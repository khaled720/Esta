using ESTA.Models;

namespace ESTA.Areas.Payment.Models
{
  
    public class PaymentReceipt
    {
        public string? Status { get; set; }

        public string? OrderId { get; set; }
        public string? OrderNumber { get; set; }

        public string? LastUpdateDate { get; set; }

        public string? Amount { get; set; }


        public Boolean IsFailed { get; set; } = true;

        public string? ErrorMsg  { get; set; }

        public Course? course { get; set; } = null;
    }
}
