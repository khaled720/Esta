using System.ComponentModel.DataAnnotations;
using ESTA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ESTA.Helpers;
using Microsoft.Extensions.Localization;

namespace ESTA.ViewModels
{
    public class RegisterViewModel 
    {
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [EmailAddress(ErrorMessageResourceName ="emailerr",ErrorMessageResourceType =typeof(ESTA.Resources.DataAnnotationsResource))]
        [RegularExpression(
            @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
            ErrorMessageResourceName = "emailformaterr",
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource)
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "email")]
        public string Email { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
       

        [DataType(DataType.Password)]
        [RegularExpression(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
          ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "passwordcons")]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessageResourceType =typeof(ESTA.Resources.DataAnnotationsResource),ErrorMessageResourceName = "confirmpassworderr")]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "confirmpassword"
        )]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string ConfirmPassword { get; set; }

        //    public bool RememberMe { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
   

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "fullname")]
        public string FullName { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "fullnamear"
        )]
        public string FullNameAr { get; set; } = String.Empty;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "birth")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public DateTime Birthdate { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "mobile")]
        public string MobilePhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "homephone")]
        public string HomePhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "englishrl")]
        public string EnglishReadingLevel { get; set; } = "Good";

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "englishwl")]
        public string EnglishWritingLevel { get; set; } = "Good";

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
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "job")]
        public string Job { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "company")]
        public string Company { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "workaddress"
        )]
        public string WorkAddress { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "workphone")]
        public string WorkPhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "workfax")]
        public string WorkFax { get; set; } = String.Empty;

        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "workleavedate"
        )]
        public DateTime? WorkLeavingDate { get; set; }

        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "workleavereasons"
        )]
        public string? WorkLeavingReasons { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "meassagingaddress"
        )]
        public string MessagingAddress { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [RegularExpression(@"(.{14})", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "nationalidcons")]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "idno")]
        public string NationalCardID { get; set; } = String.Empty;

        [RegularExpression(@"(.{8})", ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "passportcons")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "passport")]
        public string Passport { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "academicqualification"
        )]
        public string AcademicQualification { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "university"
        )]
        public string University { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "gradutionyear"
        )]
        public string GradutionYear { get; set; } = String.Empty;

        //     [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName ="required")]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "highstudies"
        )]
        public string HighStudies { get; set; } = String.Empty;

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "haveread")]
        public bool HaveRead { get; set; }
        public string codeofEthics { get; set; }


        public List<Question> Questions { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (HaveRead == false)
        //      yield  return new ValidationResult("Confirm That You have Read To Continue", new[] { nameof(HaveRead) });

        //}


        public List<UserAnswer> ConvertQuestionsToUserAnswer(string userid)
        {
            var answers = new List<UserAnswer>();

            foreach (var item in this.Questions)
            {
                var answer = "";
                if (item.IsYesNo && item.IsTrue)
                    answer = "Yes," + item.Answer;
                if (item.IsYesNo && !item.IsTrue)
                    answer = "No";
                if (!item.IsYesNo)
                    answer = item.Answer;
                answers.Add(
                    new UserAnswer
                    {
                        QuestionId = item.Id,
                        UserId = userid,
                        Answer = answer
                    }
                );
            }

            return answers;
        }
    }
}
