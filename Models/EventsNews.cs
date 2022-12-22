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
        public int Flag { get; set; } //to differentiate between events and news.1->news, 0->event
        public DateTime? Date { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
