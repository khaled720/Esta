namespace ESTA.Areas.Payment.Data
{
    public class FawryResponse
    {
        public string Type { get; set; }
        public string ReferenceNumber { get; set; }
        public string OrderStatus { get; set; }
        public string MerchantRefNumber { get; set; }
        public string StatusDescription { get; set; }
        public double FawryFees { get; set; }
    }
}
