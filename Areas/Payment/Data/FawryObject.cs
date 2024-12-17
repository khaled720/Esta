using System.Security.Cryptography;
using System.Text;

namespace ESTA.Areas.Payment.Data
{
    public class FawryObject
    {
        public FawryObject() => ChargeItems = new List<ChargeItem>();

        public string MerchantCode { get; set; }//The merchant code provided by FawryPay
        public string MerchantRefNum { get; set; }//The merchant's transaction reference number
        public string CustomerMobile { get; set; }//The customer mobile in merchant system: 01xxxxxxx.
        public string CustomerEmail { get; set; }//The customer e-mail in merchant system: test@email.com.
        public string CustomerName { get; set; }//The customer name in merchant system.
        public string CustomerProfileId { get; set; }//The unique customer profile ID in merchant system. This can be the user ID.
        public List<ChargeItem> ChargeItems { get; set; }
        public double PaymentExpiry { get; set; }//This element take value as timestamp in the format of date in milliseconds
        //public string PaymentMethod { get; set; }//Payment Method: 'CashOnDelivery', 'PayAtFawry', 'MWALLET', 'CARD' or 'VALU'.
        public string ReturnUrl { get; set; }//After the the checkout process, this will be the redirection URL where your customer will be redirected to. 
        //public bool AuthCaptureModePayment { get; set; }//Set to "true" to enable authenticate and capture payment option.
        public string Signature { get; set; }//To avoid the request from being edited by the customer use the request signature
                                             //concatenate the following elements on the same order and hash the result using SHA-256 "merchantCode + merchantRefNum + customerProfileId (if exists, otherwise insert "") + returnUrl + itemId + quantity + Price (in tow decimal format like ‘10.00’) + Secure hash key 
        public void CreateFawrySignature(string SecureKey)
        {
            using SHA256 sha256Hash = SHA256.Create();
            var StrConcat = string.Concat(this.MerchantCode, this.MerchantRefNum, this.CustomerProfileId, this.ReturnUrl);

            foreach (var item in this.ChargeItems)
            {
                StrConcat = string.Concat(StrConcat, item.ItemId, item.Quantity, item.Price + SecureKey);
            }

            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(StrConcat));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            this.Signature = builder.ToString();
        }
    }
    public class ChargeItem
    {
        public int ItemId { get; set; }//The unique product identifier.
        public string Price { get; set; }
        public int Quantity { get; set; }
        //public string Description { get; set; }//Description of charge item.
    }
}
