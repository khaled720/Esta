using System.Collections;
using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ICoursesRep
    {

       public Task<IEnumerable<Course>> GetAllCourses();
        public Course GetCourse(int id);

        public Task<bool> AddCourse(Course course);



    }
}
