using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;
using ESTA.Models;

namespace ESTA.ViewModels
{
    public class EditUserEducationInfoViewModel
    {
        public string Id { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "academicqualification"
        )]
        public string AcademicQualification { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "university"
        )]
        public string University { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "gradutionyear"
        )]
        public string GradutionYear { get; set; } = String.Empty;

        //     [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName ="required")]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "highstudies"
        )]
        public string HighStudies { get; set; } = String.Empty;

        public List<IFormFile> AcademicQualificationImages  { get; set; }

        public List<UserImage> userImages { get; set; } = new List<UserImage>();
    }

}
