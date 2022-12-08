using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }

        public int Grade { get; set; }


        public Course course { get; set; }


        public User user { get; set; }


        public bool  isPaid { get; set; }

        public State state { get; set; }

    }
}
