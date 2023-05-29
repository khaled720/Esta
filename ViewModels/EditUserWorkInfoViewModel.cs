using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ESTA.ViewModels
{
    public class EditUserWorkInfoViewModel
    {

        public string  Id { get; set; }

        [Required(
         ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
         ErrorMessageResourceName = "required"
     )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "job")]
        public string Job { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "company")]
        public string Company { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "workaddress"
        )]
        public string WorkAddress { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "workphone")]
        public string WorkPhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "workfax")]
        public string WorkFax { get; set; } = String.Empty;
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "workleavedate"
        )]
        public DateTime? WorkLeavingDate { get; set; }
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "workleavereasons"
        )]
        public string? WorkLeavingReasons { get; set; }




    }
}
