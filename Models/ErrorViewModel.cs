namespace ESTA.Models
{
    public class ErrorViewModel 
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Title { get; set; }
        public string Description { get; set; }
    }
}