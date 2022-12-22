using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class Question
    {

        [Key]
        public int Id { get; set; }

        public string  QuestionArtxt { get; set; }
        public string QuestionEntxt { get; set; }
        public bool IsYesNo { get; set; }

        public IEnumerable<UserAnswer>? userAnswers { get; set; }

    }


}
