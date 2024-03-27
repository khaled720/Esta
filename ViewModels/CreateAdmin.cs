using ESTA.Resources;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ESTA.ViewModels
{
    public class CreateAdmin
    {
        [Required(
                 ErrorMessageResourceType = typeof(DataAnnotationsResource),
        ErrorMessageResourceName = "required"
             )]
        [Display(ResourceType = typeof(DataAnnotationsResource), Name = "email")]
        public string Email { get; set; }
        [Display(ResourceType = typeof(DataAnnotationsResource), Name = "password")]
        [Required(ErrorMessageResourceType = typeof(DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessageResourceType = typeof(DataAnnotationsResource), ErrorMessageResourceName = "required")]
        [Display(ResourceType = typeof(DataAnnotationsResource), Name = "confirmpassword")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
