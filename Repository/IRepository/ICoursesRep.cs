using System.Collections;
using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ICoursesRep
    {

       public Task<IEnumerable<Course>> GetAllCourses();
        public Task<Course> GetCourse(int id);

        public Task<bool> AddCourse(Course course);

        public Task<bool> EditCourse(Course course);
        public bool DeleteCourse(int id);
       public Task<string> SearchForCourse(string Name);


    }
}
