using ESTA.Helpers;
using ESTA.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Areas.Payment.Models
{
    public class MempershipPayment
    {

        [Required]
        [Key]
        [ForeignKey("Order")]

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "orderid")]
        public int OrderId { get; set; }
        public MempershipOrder Order { get; set; }

        public User User { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "descar")]
        public string UserId { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "cdate")]
        public string CreationTime { get; set; } = String.Empty;

        //public string OrderNumber { get; set; } = string.Empty;

        //public string Currency { get; set; } = "EGP";
        /// <summary>
        /// public double Amount { get; set; }
        /// </summary>
        ///  public string OrderDescription { get; set; } = string.Empty;
        ///   public string OrderReference { get; set; } = string.Empty;
        ///    public string TransactionReference { get; set; } = string.Empty;

        //   [Column(TypeName = "ntext")]
        //   public string PrepareJsonResponse { get; set; } = string.Empty;

        //   public string OrderResult { get; set; } = string.Empty;

        ///   public string SessionId { get; set; } = string.Empty; //save that 

        ///   public string SuccessIndicator { get; set; } = string.Empty; //we save that in session

        //     [Column(TypeName = "ntext")]
        ///    public string PostOrderJsonResponse { get; set; } = string.Empty;


        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "authamount")]
        public double TotalAuthorizedAmount { get; set; }


        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "capamount")]
        public double TotalCapturedAmount { get; set; }


        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "refamount")]
        public double TotalRefundedAmount { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "cardtype")]
        public string CardType { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "nameoncard")]
        public string NameOnCard { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "lastdate")]
        public string LastUpdateTime { get; set; } = string.Empty;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "status")]
        public string Status { get; set; } = string.Empty;



        [Column(TypeName = "ntext")]
        public string ResultJsonResponse { get; set; } = string.Empty;




        public void BuildPayment(string response)
        {
            var jObj = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

            try
            {

         
      //      this.Amount = Convert.ToDouble(jObj["amount"]);
         //   this.Currency = jObj["currency"].ToString();
            this.NameOnCard = jObj["sourceOfFunds"]["provided"]["card"]["nameOnCard"].ToString();
                this.CreationTime = DateTime.Now.ToString(); // jObj["creationTime"].ToString();
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
