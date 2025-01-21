using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class GlobalConstants
    {

        public int Id { get; set; }
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "memfee")]

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public double MempershipFee { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "latepen")]

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public double LatePenalty { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "penaltymonth")]

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
            )]
        public DateTime PenaltyMonth { get; set; } = DateTime.Today;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "newmemfee")]

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public double NewMempershipFee { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "renewfee")]

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public double RenewalFee { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "memexpiry")]

        [Required(
        ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        ErrorMessageResourceName = "required"
    )]
        [RegularExpression("1[0-2]|[1-9]",
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        ErrorMessageResourceName = "memmonthexpiryerr")]
        public int MempershipExpiryMonth { get; set; }


    }
}
