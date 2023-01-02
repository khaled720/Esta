using System.ComponentModel.DataAnnotations;
using ESTA.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.ViewModels
{
    public class RegisterViewModel //:IValidatableObject
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(
            @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessage = "Not Supported Email Format"
        )]
        public string Email { get; set; }

        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6
        )]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //    public bool RememberMe { get; set; }

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
        public string EnglishReadingLevel { get; set; } = "Good";

        [Required]
        public string EnglishWritingLevel { get; set; } = "Good";

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
        [RegularExpression(@"(.{14})", ErrorMessage = "National Card ID Must be 14 digits")]
        public string NationalCardID { get; set; } = String.Empty;

        [RegularExpression(@"(.{8})", ErrorMessage = "National Card ID Must be 8 digits")]
        [Required]
        public string Passport { get; set; } = String.Empty;

        [Required]
        public string AcademicQualification { get; set; } = String.Empty;

        [Required]
        public string University { get; set; } = String.Empty;

        [Required]
        public string GradutionYear { get; set; } = String.Empty;

        // [Required]
        public string HighStudies { get; set; } = String.Empty;

        public bool HaveRead { get; set; }

        public List<Question> Questions { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (HaveRead == false)
        //      yield  return new ValidationResult("Confirm That You have Read To Continue", new[] { nameof(HaveRead) });

        //}


        public List<UserAnswer> ConvertQuestionsToUserAnswer(string userid) 
        {
        
        var answers= new List<UserAnswer>();

           foreach (var item in this.Questions)
            {
                var answer="";
                if (item.IsYesNo && item.IsTrue) answer = "Yes," + item.Answer;
                if (item.IsYesNo && !item.IsTrue) answer = "No";
                if(!item.IsYesNo) answer=item.Answer;
                answers.Add(new UserAnswer {QuestionId=item.Id,UserId=userid,Answer= answer });
            }
        
       return answers;
        }
    }
}
