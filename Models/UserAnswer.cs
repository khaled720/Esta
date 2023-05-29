using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    enum LanguageLevels
    {
        Excellent,
        VeryGood,
        Good,
        Intermediate,
        Non
    }

    //Joining Class
    public class UserAnswer
    {

        [ForeignKey("UserId")]    
        public User user { get; set; }
        public string UserId { get; set; }



        [ForeignKey("QuestionId")]
        public Question  question { get; set; }
        public int QuestionId { get; set; }




        public string? Answer { get; set; }

    }

   
}
