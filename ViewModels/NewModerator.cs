using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class NewModerator
    {
        [EmailAddress(ErrorMessageResourceName = "emailerr", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource))]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessageResourceName = "emailformaterr",ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource))]
        [Display(Name = "email")]
        [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string FullName { get; set; }

        [Display(Name = "Password")]
        [RegularExpression(
    "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&_])[A-Za-z\\d@$!%*?&_]{8,}$",
  ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "passwordcons")]
        [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "required")]
        [Compare("Password", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "confirmpassworderr")]
        public string ConfirmPassword { get; set; }
        
        public List<int> SelectForum { get; set; }
    }
}
