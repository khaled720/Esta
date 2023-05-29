using System.Collections;
using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ICoursesRep
    {

       public Task<IEnumerable<Course>> GetAllCourses();
        public  Task<IEnumerable<Course>> GetAllCoursesByLevel(int LevelId);
        public  Task<IEnumerable<Course>> GetAllOtherCourses();
        public  Task<IEnumerable<Course>> GetAllCetaCourses();

        public Task<IEnumerable<User>> GetAllCetaHolders();

        public Task<Course> GetCourse(int id);
        public Task<Course> GetUpcomingCourse();

        public Task<bool> AddCourse(Course course);

        public Task<bool> EditCourse(Course course);
        public bool DeleteCourse(int id);
       public Task<string> SearchForCourse(string Name);
		Task<List<UserCourse>> GetEnrolledUsersInCourse(int id);


    }
}
