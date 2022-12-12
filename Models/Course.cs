using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace ESTA.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Title Is Required !!!")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Payment Link Is Required !!!")]
        public string PaymentLink  { get; set; }

        public virtual Level? level { get; set; }
        [Required(ErrorMessage = "Level Is Required !!!")]
        [ForeignKey("TypeIdFornKey")]
        public int? LevelId { get; set; }

        public int Price { get; set; } = 150;


        [Required(ErrorMessage = "Start Date Is Required !!!")]
        //   [Key]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Final Grade Is Required !!!")]
        public int FinalGrade { get; set; }

        [Required(ErrorMessage = "Title Is Required !!!")]
        public string TitleAr { get; set; } 

   
        public string?  PhotoPath { get; set; }

   
        public string? Description { get; set; }


        public string? DescriptionAr { get; set; }

    }

}
