namespace ESTA.ViewModels
{
    public class DisplayEvents
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Flag { get; set; }
        public int? EventType { get; set; }
        public DateTime? Date { get; set; }
    }
}
