using ESTA.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Areas.Payment.Models
{
    public class  CoursePayment
    {

            
   
    

        [Required]
        [Key]
        [ForeignKey("Order")]

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "orderid")]
        public int OrderId { get; set; }
        public CourseOrder  Order { get; set; }

        public User User { get; set; }


        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "descar")]
        public string UserId { get; set; } = string.Empty;



        public Course Course { get; set; }


        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "descar")]
        public int  CourseId { get; set; } 





        [Column(TypeName = "ntext")]
        public string ResultJsonResponse { get; set; } = string.Empty;

        //    public double Amount { get; set; }

        //       public string Currency { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "nameoncard")]
        public string NameOnCard { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "cdate")]
        public string CreationTime { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "lastdate")]
        public string LastUpdateTime { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "status")]
        public string Status { get; set; } = string.Empty;


        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "authamount")]
        public double TotalAuthorizedAmount { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "capamount")]

        public double TotalCapturedAmount { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "refamount")]
        public double TotalRefundedAmount { get; set; }


        [Display(ResourceType = typeof(ESTA.Resources
            .DataAnnotationsResource), Name = "cardtype")]
        public string CardType { get; set; } = string.Empty;



        public void BuildPayment(string response)
        {
            try
            {

            var jObj = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);
            

           // this.Amount = Convert.ToDouble(jObj["amount"]);
           // this.Currency = jObj["currency"].ToString();
            this.NameOnCard = jObj["sourceOfFunds"]["provided"]["card"]["nameOnCard"].ToString();
            this.CreationTime = DateTime.Now.ToString();  // jObj["creationTime"].ToString();
            this.LastUpdateTime = jObj["lastUpdatedTime"].ToString();
            this.Status = jObj["status"].ToString();
            this.TotalAuthorizedAmount = Convert.ToDouble(jObj["totalAuthorizedAmount"]);
            this.TotalCapturedAmount = Convert.ToDouble(jObj["totalCapturedAmount"]);
            this.TotalRefundedAmount = Convert.ToDouble(jObj["totalRefundedAmount"]);
            this.CardType = jObj["sourceOfFunds"]["provided"]["card"]["brand"].ToString();
            this.ResultJsonResponse = response;
            }
            catch (Exception)
            {

                throw;
            }

            return;

        }
    }
}
