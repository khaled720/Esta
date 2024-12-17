using System.Security.Cryptography;
using System.Text;

namespace ESTA.Areas.Payment.Data
{
    public class FawrypayRequest
    {
        public string MerchantCode { get; set; }
        public string MerchantRefNum { get; set; }
        public string Signature { get; set; }
        public void CreateFawrySignature(string SecureKey)
        {
            using SHA256 sha256Hash = SHA256.Create();
            var StrConcat = string.Concat(this.MerchantCode, this.MerchantRefNum, SecureKey);

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
}
