using System.ComponentModel.DataAnnotations;

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
