using ESTA.Areas.Admin.Models;
using ESTA.Models;

namespace ESTA.ViewModels
{
    public class CourseDetailsViewModel
    {

        public Course   course { get; set; }

        public bool  isCourseEnrolled{ get; set; }

        public int  UsersEnrolledCount { get; set; }

        public bool IsCourseRefunded { get; set; }

        public string  userid { get; set; }

        public bool IsMempershipPaid { get; set; }
        public List<PrerequisiteCourse> PrerequisiteCourses { get;  set; }

        public bool IsPrerequisiteCoursesPassed { get; set; }



    }
}
