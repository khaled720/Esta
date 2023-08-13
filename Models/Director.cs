using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class Director
    {


        [Key]
        public int Id { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "namear")]
        public string  NameAr { get; set; }

        [Required(
             ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
             ErrorMessageResourceName = "required"
         )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "nameen")]
        public string NameEn { get; set; }


        public string PhotoPath { get; set; } = String.Empty;
        [Required(
                  ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
                  ErrorMessageResourceName = "required"
              )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "jobar")]
        public string JobAr { get; set; }

        [Required(
              ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
              ErrorMessageResourceName = "required"
          )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "joben")]
        public string JobEn { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "bioar")]
        public string BioAr { get; set; }
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "bioen")]
        public string BioEn { get; set; }


        [NotMapped]
        public IFormFile? Photo { get; set; }


    }
}
