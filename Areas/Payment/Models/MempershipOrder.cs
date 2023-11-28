using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using System.Xml.Linq;
using ESTA.Models;
using Newtonsoft.Json;

namespace ESTA.Areas.Payment.Models
{
    public class MempershipOrder
    {

        public int Id       { get; set; }
        public User? User { get; set; }
        public string UserId { get; set; } = string.Empty;


        [Display(ResourceType = typeof(ESTA.Resources
 .DataAnnotationsResource), Name = "cdate")]

        public string CreationTime { get; set; } = DateTime.Now.ToString();




        [Display(ResourceType = typeof(ESTA.Resources
 .DataAnnotationsResource), Name = "orderum")]

        public string OrderNumber    { get; set; } = string.Empty;


        [Display(ResourceType = typeof(ESTA.Resources
 .DataAnnotationsResource), Name = "curr")]
        public string Currency      { get; set; } ="EGP";

        [Display(ResourceType = typeof(ESTA.Resources
 .DataAnnotationsResource), Name = "amount")]
        public double Amount    { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources
 .DataAnnotationsResource), Name = "orderdesc")]
        public string OrderDescription      { get; set; } = string.Empty;
        [Display(ResourceType = typeof(ESTA.Resources
.DataAnnotationsResource), Name = "orderref")]
        public string OrderReference    {   get; set; } = string.Empty;
        [Display(ResourceType = typeof(ESTA.Resources
.DataAnnotationsResource), Name = "transref")]
        public string TransactionReference   { get; set; } = string.Empty;

        [Column(TypeName ="ntext")]
        public string  PrepareJsonResponse         { get; set; } = string.Empty;
        [Display(ResourceType = typeof(ESTA.Resources
.DataAnnotationsResource), Name = "status")]
        public string OrderResult       { get; set; } = string.Empty;

        public string SessionId         { get; set; } = string.Empty; //save that 

        public string SuccessIndicator  { get; set; } = string.Empty; //we save that in session

        [Column(TypeName = "ntext")]
        public string PostOrderJsonResponse { get; set; } = string.Empty;




        public void BuildOrder(string response)
        {
            try
            {
                Random random = new Random();

                this.OrderNumber = random.Next().ToString();
                this.Currency = "EGP";

                this.OrderDescription =  DateTime.Now.ToShortDateString()+"  Mempership Order for";
                this.Amount = 2500;
        
            }
            catch (Exception ex)
            {

                // new LogManager(hostEnvironment).WriteInLogFile

            }

            return;

        }
        public void SetDataAfterPrepareOrder(string response)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

                this.TransactionReference = obj["transaction"]["reference"];

                this.PrepareJsonResponse = response;

                this.OrderReference = obj["order"]["reference"];
            }
            catch (Exception ex)
            {
                //             LogManager.WriteInLogFile(ex.Message);

            }

            return;

        }




        public void SetDataAfterPostOrder(string response)
        {
            try
            {
                var obj2 = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

                this.SuccessIndicator = obj2["successIndicator"];

                this.SessionId = obj2["session"]["id"];
                this.OrderResult = obj2["result"];
                this.PostOrderJsonResponse = response;
                
            }
            catch (Exception ex)
            {
                //       LogManager.WriteInLogFile(ex.Message);

            }

            return;

        }

    }
}
