using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }

        public int Grade { get; set; }


        public Course course { get; set; }

        [ForeignKey("CourseIdFK")]
        public int CourseId { get; set; }
        public User user { get; set; }

        [ForeignKey("UserIdFKey")]
        public string UserId { get; set; }
        public bool  isPaid { get; set; }

        public State state { get; set; }
        [ForeignKey("StateFK")]
        public int StateId { get; set; }
    }
}
