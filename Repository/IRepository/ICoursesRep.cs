using System.Collections;
using ESTA.Areas.Admin.Models;
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

        public Task<bool> IsCourseEnrolledByUser(int courseId,string userId);


        public Task<bool> UpdateCourseState(int courseId,string userId,int StateId);


        public Task<bool> EditCourse(Course course);
        public bool DeleteCourse(int id);
        public Task<string> SearchForCourse(string Name);
		Task<List<UserCourse>> GetEnrolledUsersInCourse(int id);
        Task<List<Course>> SearchCoursesByName(string query);
        Task<int> GetEnrolledUsersInCourseLength(int courseId);

        Task<bool> AddPrerequisiteCourses(List<PrerequisiteCourse> prerequisiteCourses);
        Task<bool> IsPrerequisiteCourse(int MainCourseId,int PreCourseId);
        Task<List<PrerequisiteCourse>> GetPrerequisiteCourses(int MainCourseId);
    }
}
