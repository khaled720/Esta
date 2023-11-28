using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUsersCoursesRep
    {

        public Task<bool> RemovePaylaterUsersExceeded3days();
        public List<UserCourse> GetAllUsersEnrolledinCourse(int courseId);
        public bool UpdateUserCoursePaymentStatus(string UserId, int CourseId, bool NewState);

        // List<Course>  GetCurrentUserCourses();

       public Task<List<UserCourse>>  GetUserCoursesResults(string userId);


        public Task<bool> IsCourseRefunded(int courseId,string userId);


        Task<bool> UpdateUserCourseResult(int courseId, string userId,int grade);
        Task<UserCourse>  GetUserCourse(int courseId, string userId);
    }
}
