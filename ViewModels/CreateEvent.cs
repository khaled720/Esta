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
        [Required(ErrorMessage = "The Event or news field is required")]
        public int Flag { get; set; } //to differentiate between events and news. 1->news, 0->event
        public int? EventType { get; set; } //to differentiate between social and esta events. 1->esta, 0->social
        public DateTime? Date { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
