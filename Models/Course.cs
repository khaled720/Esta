using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ESTA.Areas.Admin.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace ESTA.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "titleen")]
        public string Title { get; set; }

        //[Required(
        //    ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        //    ErrorMessageResourceName = "required"
        //)]
        //[Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "paylink")]
        //public string PaymentLink { get; set; } = "";

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "level")]

        public virtual Level? level { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [ForeignKey("TypeIdFornKey")]
        public int? LevelId { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "price")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Range(
            0,
            10000,
            ErrorMessageResourceName = "pricerange",
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource)
        )]
        public int Price { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        //   [Key]

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "stdate")]
        public DateTime? StartDate { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "fngrade")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public int FinalGrade { get; set; } = 100;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "succgrade")]
        [Required(
          ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
          ErrorMessageResourceName = "required"
      )]
        
        public int SuccessPersentage { get; set; } = 50;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "titlear")]
        public string TitleAr { get; set; }

        public string? PhotoPath { get; set; }
        [Required(
          ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
          ErrorMessageResourceName = "required"
      )]

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "descen")]
        public string? Description { get; set; }
        [Required(
          ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
          ErrorMessageResourceName = "required"
      )]

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "descar")]
        public string? DescriptionAr { get; set; }



        [Required(
        ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        ErrorMessageResourceName = "required"
    )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "maxcoursemembers")]
        [RegularExpression("[1-9][0-9]{1,3}", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        ErrorMessageResourceName = "maxallowedmemberserr")]
        public int MaxAllowedMembersCount { get; set; } = 100;







  //      public IEnumerable<PrerequisiteCourse>? PrerequisiteCourses { get; set; }

        public IEnumerable<UserCourse>? users { get; set; }
    }
}
