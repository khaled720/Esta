using ESTA.Resources;
using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class ChangePassword
    {
        [Display(ResourceType = typeof(DataAnnotationsResource), Name = "currentpassword")]
        [Required(ErrorMessageResourceType = typeof(DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string CurrentPassword { get; set; }
        [Display(ResourceType = typeof(DataAnnotationsResource), Name = "password")]
        [Required(ErrorMessageResourceType = typeof(DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessageResourceType = typeof(DataAnnotationsResource), ErrorMessageResourceName = "required")]
        [Display(ResourceType = typeof(DataAnnotationsResource),Name = "confirmpassword")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
