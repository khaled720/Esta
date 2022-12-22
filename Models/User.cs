using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ESTA.Models
{
    public class User :IdentityUser
    {

        [Display(Name ="Full Name In English")]
        [Required]
        public string FullName { get; set; } = String.Empty;





        /// New Added Props <summary >
        /// 

        [Display(Name = "Full Name In Arabic")]
        [Required]
        public string FullNameAr { get; set; } = String.Empty;




        [Required]
        public DateTime Birthdate { get; set; }


        [Required]
        public string MobilePhone { get; set; } = String.Empty;
        [Required]
        public string HomePhone { get; set; } = String.Empty;
        [Required]
        public string EnglishReadingLevel { get; set; } = String.Empty;

        [Required]
        public string EnglishWritingLevel { get; set; } = String.Empty;



        [Required]
        public string Hometown { get; set; } = String.Empty;
        //  [Display(Name = "Street Name")]
        [Required]
        public string StreetName { get; set; } = String.Empty;


        [Required]
        public string BlockNumber { get; set; } = String.Empty;


        [Required]
        public string Floor { get; set; } = String.Empty;

        [Required]
        public string FlatNumber { get; set; } = String.Empty;

        [Required]
        public string Area { get; set; } = String.Empty;

        [Required]
        public string City { get; set; } = String.Empty;

        [Required]
        public string Country { get; set; } = String.Empty;



        [Required]
        public string Job { get; set; } = String.Empty;

        [Required]
        public string Company { get; set; } = String.Empty;
        [Required]
        public string WorkAddress { get; set; } = String.Empty;
        [Required]
        public string WorkPhone { get; set; } = String.Empty;
        [Required]
        public string WorkFax { get; set; } = String.Empty;


        public DateTime? WorkLeavingDate { get; set; }

        public string? WorkLeavingReasons { get; set; }

        [Required]
        public string MessagingAddress { get; set; } = String.Empty;






        [Required]
        public string NationalCardID { get; set; } = String.Empty;

        [Required]
        public string Passport { get; set; } = String.Empty;


        [Required]
        public string AcademicQualification { get; set; } = String.Empty;

        [Required]
        public string University { get; set; } = String.Empty;

        [Required]
        public string HighStudies { get; set; } = String.Empty;




        /// </summary>




        [Required]
        public bool IsMempershipPaid { get; set; }


        //[ForeignKey("ForumLevelFrnKey")]
        //public int ForumLevelId { get; set; }

        
        public DateTime JoinDate { get; set; } = DateTime.Now;

    

        [ForeignKey("LevelId")]
        public virtual Level level { get; set; }

        
        public int LevelId { get; set; }

        public IEnumerable<UserCourse>? Courses { get; set; }
        public IEnumerable<UserForum>? Forums { get; set; }

        public IEnumerable<UserAnswer>? userAnswers { get; set; }


    }
}
