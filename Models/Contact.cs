using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TitleEn { get; set; }
        [Required]
        public string TitleAr { get; set; }


        [Required]
        public string AddressEn { get; set; }
        [Required]
        public string AddressAr { get; set; }

        [Required]
        public string PhoneLines { get; set; }
        [Required]
        public string Emails { get; set; }


    }
}
