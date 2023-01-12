using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class EditEvents
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.EventsNews)), ErrorMessageResourceName = "TitleEnError")]
        public string TitleEn { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.EventsNews)), ErrorMessageResourceName = "TitleArError")]
        public string TitleAr { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.EventsNews)), ErrorMessageResourceName = "DetailsEnError")]
        public string DetailsEn { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.EventsNews)), ErrorMessageResourceName = "DetailsArError")]
        public string DetailsAr { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.EventsNews)), ErrorMessageResourceName = "FlagError")]
        public int Flag { get; set; } //to differentiate between events and news. 1->news, 0->event
        public int? EventType { get; set; } //to differentiate between social and esta events. 1->esta, 0->social
        public DateTime? Date { get; set; }
        public string Image { get; set; }
        public IFormFile? ImageUpload { get; set; } = null;

    }
}
