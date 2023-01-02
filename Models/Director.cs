using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class Director
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public string  NameAr { get; set; }

        [Required]
        public string NameEn { get; set; }


        public string PhotoPath { get; set; } = String.Empty;

        [Required]
        public string JobAr { get; set; }

        [Required]
        public string JobEn { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }


    }
}
