using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class State
    
    {

        [Key]
        public int Id { get; set; }
        public string StateName { get; set; }

        public IEnumerable<UserCourse> userCourses { get; set; }

    }
}
