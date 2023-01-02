using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class UserCourse
    {
       

        public int Grade { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public Course course { get; set; }

        [ForeignKey("CourseId")]
        public int CourseId { get; set; }


        public User user { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public bool  isPaid { get; set; }
        


        [ForeignKey("StateId")]
        public State state { get; set; }
      
        public int StateId { get; set; }
    }
}
