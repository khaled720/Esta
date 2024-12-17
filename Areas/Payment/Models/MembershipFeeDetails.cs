using ESTA.Models;
using Microsoft.AspNetCore.Identity;

namespace ESTA.Areas.Payment.Models
{
    public class MembershipFeeDetails
    {
        public double RenewalFee { get; set; }
        public double NewMempershipFee { get; set; }
        public double LatePenalty { get; set; }
        public double MembershipFee { get; set; }
        public double Total { get; set; }
        public string PaymentMethod { get; set; }

    }
}
