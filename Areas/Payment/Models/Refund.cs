using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;
using ESTA.Models;

namespace ESTA.Areas.Payment.Models
{
    public class Refund
    {

        public int Id { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "orderum")]
        public string  OrderNumber { get; set; }
        [Display(ResourceType = typeof(ESTA.Resources
        .DataAnnotationsResource), Name = "serialnum")]
        public string SerialNumber { get; set; }
        [Display(ResourceType = typeof(ESTA.Resources
        .DataAnnotationsResource), Name = "reqamount")]
        public double  RequestedAmount { get; set; }
        [Display(ResourceType = typeof(ESTA.Resources
        .DataAnnotationsResource), Name = "cdate")]
        public string CreateDate { get; set; } = DateTime.Now.ToString();

        public User User { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Display(ResourceType = typeof(ESTA.Resources
        .DataAnnotationsResource), Name = "status")]
        public string  Status  { get; set; }
        [Display(ResourceType = typeof(ESTA.Resources
        .DataAnnotationsResource), Name = "reason")]
        public string  Reason  { get; set; }
        [Display(ResourceType = typeof(ESTA.Resources
        .DataAnnotationsResource), Name = "type")]
        public string Type { get; set; }  //course or mempership

    }
 public   enum RefundStates
    {
        Pending,
        InProgress,
        Refunded,
        Rejected

    }
 public    enum RefundTypes
    {
    Course,
    Mempership

    }

}
