using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class LoginViewModel
    {

        [Required(
                 ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
                 ErrorMessageResourceName = "required"
             )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "email")]
        public string Email { get; set; }

        [Required(
             ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
             ErrorMessageResourceName = "required"
         )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "password")]
        public string Password { get; set; }


        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "remember")]
        public bool RememberMe { get; set; }
    }
}
