using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class CreateEvent
    {
        [Required]
        public string TitleEn { get; set; }
        [Required]
        public string TitleAr { get; set; }
        [Required]
        public string DetailsEn { get; set; }
        [Required]
        public string DetailsAr { get; set; }
        public int Flag { get; set; } //to differentiate between events and news. 1->news, 0->event
        public DateTime? Date { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
