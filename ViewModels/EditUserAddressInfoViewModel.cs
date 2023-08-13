using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ESTA.ViewModels
{
    public class EditUserAddressInfoViewModel
    {


        [Required(
                   ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        ErrorMessageResourceName = "required"
               )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "hometown")]
        public string Hometown { get; set; } = String.Empty;

        //  [Display(Name = "Street Name")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "stname")]
        public string StreetName { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "blockno")]
        public string BlockNumber { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "floor")]
        public string Floor { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "flatno")]
        public string FlatNumber { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "area")]
        public string Area { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "city")]
        public string City { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "country")]
        public string Country { get; set; } = String.Empty;

        [Required(
    ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
    ErrorMessageResourceName = "required"
)]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "postalcode")]
        public string PostalCode { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "meassagingaddress"
        )]
        public string MessagingAddress { get; set; } = String.Empty;


        public string Id { get; set; }


    }
}
