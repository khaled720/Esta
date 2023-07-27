using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ESTA.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Full Name In English")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string FullName { get; set; } = String.Empty;

        /// New Added Props <summary >
        ///

        [Display(Name = "Full Name In Arabic")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string FullNameAr { get; set; } = String.Empty;

        public bool IsApproved { get; set; } = false;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public DateTime Birthdate { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string MobilePhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string HomePhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string EnglishReadingLevel { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string EnglishWritingLevel { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string Hometown { get; set; } = String.Empty;

        //  [Display(Name = "Street Name")]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string StreetName { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string BlockNumber { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string Floor { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string FlatNumber { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string Area { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string City { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string Country { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string Job { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string Company { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string WorkAddress { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string WorkPhone { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string? WorkFax { get; set; } = String.Empty;

        public DateTime? WorkLeavingDate { get; set; }

        public string? WorkLeavingReasons { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string MessagingAddress { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string? NationalCardID { get; set; } = String.Empty;

        //  [Required(
        //    ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        //    ErrorMessageResourceName = "required"
        //)]
        public string? Passport { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string AcademicQualification { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string University { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public string? HighStudies { get; set; } = String.Empty;

        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(
            ResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            Name = "gradutionyear"
        )]
        public string GradutionYear { get; set; } = String.Empty;

        /// </summary>


        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        public bool IsMempershipPaid { get; set; }

        //[ForeignKey("ForumLevelFrnKey")]
        //public int ForumLevelId { get; set; }


        public DateTime JoinDate { get; set; } = DateTime.Now;

        [ForeignKey("LevelId")]
        public virtual Level level { get; set; }

        public int LevelId { get; set; }
        public virtual ICollection<UserForum> userForum { get; set; }

        public IEnumerable<UserCourse>? Courses { get; set; }

        public IEnumerable<UserAnswer>? userAnswers { get; set; }



        /// new Added Features ....
        public string? NationalIDImagePath { get; set; } = String.Empty;
        public string?  PassportImagePath { get; set; } = String.Empty;
        public string GradutionImagePath { get; set; } = String.Empty;
        public string? MembershipNumber { get; set; } = String.Empty;

        /// /////////////



        public void ConvertRegisterModelToUser(RegisterViewModel registerViewModel)
        {
            this.UserName = registerViewModel.Email;
            this.Email = registerViewModel.Email;
            this.FullName = registerViewModel.FullName;
            this.FullNameAr = registerViewModel.FullNameAr;
            this.Birthdate = registerViewModel.Birthdate;
            this.Passport = registerViewModel.Passport;
            this.NationalCardID = registerViewModel.NationalCardID;
            this.HomePhone = registerViewModel.HomePhone;
            this.MobilePhone = registerViewModel.MobilePhone;
            this.AcademicQualification = registerViewModel.AcademicQualification;
            this.University = registerViewModel.University;
            this.HighStudies = registerViewModel.HighStudies;


            this.MembershipNumber = registerViewModel.MembershipNumber;
          
            
            this.MessagingAddress = registerViewModel.MessagingAddress;
            this.Job = registerViewModel.Job;
            this.Company = registerViewModel.Company;
            this.WorkPhone = registerViewModel.WorkPhone;
            this.WorkFax = registerViewModel.WorkFax;
            this.WorkAddress = registerViewModel.WorkAddress;
            this.WorkLeavingReasons = registerViewModel.WorkLeavingReasons;
            this.WorkLeavingDate = registerViewModel.WorkLeavingDate;

            this.Country = registerViewModel.Country;
            this.Area = registerViewModel.Area;
            this.City = registerViewModel.City;
            this.Hometown = registerViewModel.Hometown;
            this.IsMempershipPaid = false;

            this.EnglishReadingLevel = registerViewModel.EnglishReadingLevel;
            this.EnglishWritingLevel = registerViewModel.EnglishWritingLevel;

            this.StreetName = registerViewModel.StreetName;
            this.BlockNumber = registerViewModel.BlockNumber;
            this.FlatNumber = registerViewModel.FlatNumber;
            this.Floor = registerViewModel.Floor;
            this.LevelId = 4;

            return;
        }
    }
}
