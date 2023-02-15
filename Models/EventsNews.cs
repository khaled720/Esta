using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class EventsNews
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string TitleEn { get; set; }
        [StringLength(100)]
        public string TitleAr { get; set; }
        public string Image { get; set; }
        public string DetailsEn { get; set; }
        public string DetailsAr { get; set; }
        public int Flag { get; set; } //to differentiate between events and news. 1->news, 0->event
        public int? EventType { get; set; } //to differentiate between social and esta events. 1->esta, 0->social
        public DateTime? Date { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
