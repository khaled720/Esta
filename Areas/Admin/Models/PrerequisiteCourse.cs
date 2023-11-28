using System.ComponentModel.DataAnnotations.Schema;
using ESTA.Models;

namespace ESTA.Areas.Admin.Models
{
    //Joining Table
    public class PrerequisiteCourse
    {
       // [NotMapped]
        [ForeignKey("MainCourseId")]
        public Course MainCourse { get; set; }
  

        public int MainCourseId { get; set; }


        //[NotMapped]  
        [ForeignKey("PrerequisiteCourseId")]
        public Course prerequisiteCourse { get; set; }
      

        public int PrerequisiteCourseId { get; set; }



        [NotMapped]
        public bool isPassed { get; set; }

    }
}
