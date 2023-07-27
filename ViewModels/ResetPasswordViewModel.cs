using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ESTA.ViewModels
{
    public class ResetPasswordViewModel
    {

        public string? Token { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "email")]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(
          "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&_])[A-Za-z\\d@$!%*?&_]{8,}$",
        ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "passwordcons")]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "password")]

        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "confirmpassworderr")]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "confirmpassword")]

        public string ConfirmNewPassword { get; set; }
   
        public ResetPasswordViewModel()
        {

        }

        public ResetPasswordViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }


    }


}
