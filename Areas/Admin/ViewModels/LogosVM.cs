using ESTA.Resources;
using System.ComponentModel.DataAnnotations;

namespace ESTA.Areas.Admin.ViewModels
{
    public class LogosVM
    {
        //public string FilePath { get; set; }
        [Required(
 ErrorMessageResourceType = typeof(DataAnnotationsResource),
ErrorMessageResourceName = "required"
)]
        public IFormFile ImageFile { get; set; }
    }
}
