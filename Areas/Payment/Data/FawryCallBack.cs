namespace ESTA.Areas.Payment.Data
{
    public class FawryCallBack
    {
        public string requestId { get; set; }          //"requestId":"c72827d084ea4b88949d91dd2db4996e",
        public string fawryRefNumber { get; set; }          //"fawryRefNumber":"970177",
        public string merchantRefNumber { get; set; }          //"merchantRefNumber":"9708f1cea8b5426cb57922df51b7f790",
        public string customerMobile { get; set; }          //"customerMobile":"01004545545",
        public string customerMail { get; set; }          //"customerMail":"fawry@fawry.com",
        public double paymentAmount { get; set; }          //"paymentAmount":152.00,
        public double orderAmount { get; set; }          //"orderAmount":150.00,
        public double fawryFees { get; set; }          //"fawryFees":2.00,
        public double? shippingFees { get; set; }          //"shippingFees":null,
        public string orderStatus { get; set; }          //"orderStatus":"NEW",
        public string paymentMethod { get; set; }          //"paymentMethod":"PAYATFAWRY",
        public string messageSignature { get; set; }          //"messageSignature":"56bca514b2cc6822bf972a869a008f03cacebb14d19829368daa647dbc212aa5",
        public string orderExpiryDate { get; set; }          //"orderExpiryDate":1533554719314,
        public List<ChargeItem> orderItems { get; set; }          //"orderItems":[{}]
        public ThreeDSInfo? threeDSInfo { get; set; }          //"threeDSInfo": {}
        public InvoiceInfo invoiceInfo { get; set; }          // "invoiceInfo":{}
        public double installmentInterestAmount { get; set; }          //"installmentInterestAmount":0.0,
        public int installmentMonths { get; set; }         //  "installmentMonths":6
    }
    public class InvoiceInfo
    {
        public string number { get; set; }//"number": "28176849",
        public string businessRefNumber { get; set; }//"businessRefNumber": "w0dd2fss41d2d2qs556",
        public string dueDate { get; set; }//"dueDate": "2021-06-19",
        public double expiryDate { get; set; }//"expiryDate": 1625062277000
    }
    public class ThreeDSInfo
    {
        public string eci { get; set; }//"eci": "05",
        public string xid { get; set; }//"xid": "VDj97t1qRJWM0ErrY2PtrBiSMQw=",
        public string enrolled { get; set; }//"enrolled": "Y",
        public string status { get; set; }//"status": "Y",
        public string batchNumber { get; set; }//"batchNumber": "0",
        public string command { get; set; }//"command": "pay",
        public string message { get; set; }//"message": "Approved",
        public string verSecurityLevel { get; set; }//"verSecurityLevel": "05",
        public string verStatus { get; set; }//"verStatus": "Y",
        public string verType { get; set; }//"verType": "3DS",
        public string verToken { get; set; }//"verToken": "gIGCg4SFhoeIiYqLjI2Oj5CRkpM=",
        public string version { get; set; }//"version": "1",
        public string receiptNumber { get; set; }//"receiptNumber": "1123456",
        public string sessionId { get; set; }//"sessionId": "SESSION0002818019663G5075633E86"
    }
}
