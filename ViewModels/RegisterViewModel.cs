using System.ComponentModel.DataAnnotations;
using ESTA.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        public bool RememberMe { get; set; }

        [Display(Name = "Full Name In English")]
        [Required]
        public string FullName { get; set; } = String.Empty;
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
        public string GradutionYear { get; set; } = String.Empty;

        [Required]
        public string HighStudies { get; set; } = String.Empty;
        public int LevelId { get; set; }

        public IEnumerable<Level> Levels { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        public IEnumerable<UserAnswer> UserAnswers { get; set; }


    }
}
