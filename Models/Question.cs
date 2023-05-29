using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class Question :IValidatableObject
    {

        [Key]
        public int Id { get; set; }

        public string  QuestionArtxt { get; set; }
        public string QuestionEntxt { get; set; }
        public bool IsYesNo { get; set; }
    
        public IEnumerable<UserAnswer>? userAnswers { get; set; }



        //[Required(
        //          ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
        //          ErrorMessageResourceName = "required"
        //      )]
        [NotMapped]
        public string? Answer { get; set; } = "";

        [NotMapped]
        public bool IsTrue { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Answer == null && IsTrue == true)
                yield return new ValidationResult("Yes Answer Is Required.....", new[] { nameof(Answer) });
        }
    }


}
