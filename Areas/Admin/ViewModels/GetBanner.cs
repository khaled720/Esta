using ESTA.Resources;
using System.ComponentModel.DataAnnotations;

namespace ESTA.Areas.Admin.ViewModels
{
    public class GetBanner
    {
        public int Type { get; set; }
        public string FilePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required(
         ErrorMessageResourceType = typeof(DataAnnotationsResource),
ErrorMessageResourceName = "required"
     )]
        public string SloganAr { get; set; }
        [Required(
         ErrorMessageResourceType = typeof(DataAnnotationsResource),
ErrorMessageResourceName = "required"
     )]
        public string SloganEn { get; set; }
        public string DetailsAr { get; set; }
        public string DetailsEn { get; set; }
    }
}
