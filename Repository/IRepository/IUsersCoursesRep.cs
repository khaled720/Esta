using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUsersCoursesRep
    {


        public bool UpdateUserCoursePaymentStatus(string UserId, int CourseId, bool NewState);

        // List<Course>  GetCurrentUserCourses();

       public Task<List<UserCourse>>  GetUserCoursesResults(string userId);

    }
}
