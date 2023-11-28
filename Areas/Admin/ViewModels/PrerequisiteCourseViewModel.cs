using ESTA.Models;

namespace ESTA.Areas.Admin.ViewModels
{
    public class PrerequisiteCourseViewModel
    {
        
        public Course MainCourse { get; set; }
        public List<PreCourse>   PreCourses { get; set; }

       

    }


 public   class PreCourse{

        
        public bool isPrerequisite { get; set; }

        public Course course { get; set; }


    }
}
