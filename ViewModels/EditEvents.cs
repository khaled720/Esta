using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class EditEvents
    {
        public int Id { get; set; }
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
        public string Image { get; set; }
        public IFormFile? ImageUpload { get; set; } = null;
    }
}
