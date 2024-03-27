using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;
using ESTA.Models;

namespace ESTA.ViewModels
{
    public class EditUserPersonalInfoViewModel
    {

        public string  Id { get; set; }


        [Required(
           ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
           ErrorMessageResourceName = "required"
       )]
        [EmailAddress(ErrorMessageResourceName = "emailerr", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource))]
        [RegularExpression(
           @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
           ErrorMessageResourceName = "emailformaterr",
           ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource)
       )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "email")]
        public string Email { get; set; }




        [Required(
         ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        ErrorMessageResourceName = "required"
     )]

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "fullname")]


        public string FullName { get; set; } = String.Empty;





        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "fullnamear"
        )]
        public string FullNameAr { get; set; } = String.Empty;







        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "birth")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public DateTime Birthdate { get; set; } = DateTime.Now;







        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "mobile")]
        public string MobilePhone { get; set; } = String.Empty;





        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "homephone")]
        public string HomePhone { get; set; } = String.Empty;






        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [RegularExpression(@"(.{14})", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "nationalidcons")]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "idno")]
        public string NationalCardID { get; set; } = String.Empty;

        //[RegularExpression(@"(.{8})", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        //    ErrorMessageResourceName = "passportcons")]
        //[Required(
        //    ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        //    ErrorMessageResourceName = "required"
        //)]
        [Display(ResourceType = typeof(Resources.DataAnnotationsResource), Name = "passport")]
        public string? Passport { get; set; } = String.Empty;

        [Display(ResourceType = typeof(Resources.DataAnnotationsResource), Name = "passportImg")]
        public List<IFormFile>?  PassportImages { get; set; }
        [Display(ResourceType = typeof(Resources.DataAnnotationsResource), Name = "NationalIdImagesImg")]
        public List<IFormFile>?  NationalIdImages { get; set; }

        public List<UserImage> userImages { get; set; } =new List<UserImage>();

       // [Required(
       //    ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
       //    ErrorMessageResourceName = "required"
       //)]
        public string MembershipNumber { get; set; } = String.Empty;


    }
}
