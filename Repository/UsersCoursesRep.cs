using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class UsersCoursesRep : IUsersCoursesRep
    {
        private readonly AppDbContext appDbContext;

        public UsersCoursesRep(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }



public List<UserCourse>     GetAllUsersEnrolledinCourse(int courseId)
            {

      return     appDbContext.UserCourses.AsQueryable().Where(y => y.CourseId == courseId).Include(y => y.user).ToList();


}

        public async Task<UserCourse>  GetUserCourse(int courseId, string userId)
        {
            try
            {

                return await appDbContext.UserCourses.AsQueryable()
                                 .AsTracking()
                    .Where(y => y.CourseId == courseId && y.UserId == userId).FirstAsync();

   
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<UserCourse>> GetUserCoursesResults(string userId)
        {
            try
            {
                return await appDbContext.UserCourses.Where(y=>y.UserId==userId&&(y.StateId==3|| y.StateId == 5)).Include(y=>y.course).ToListAsync();
            }
            catch (Exception)
            {

                return new List<UserCourse> { };
            }
        }

        public async Task<bool> IsCourseRefunded(int courseId, string userId)
        {
            try
            {
             var usercourse=await   appDbContext.UserCourses.AsQueryable()
                    .Where(y => y.UserId == userId && y.CourseId == courseId).FirstAsync();

                if (usercourse.StateId==4)
                {
             return true;
                }
            }
            catch (Exception)
            {

            return false;

            }
            return false;
        }

        public  async Task<bool> RemovePaylaterUsersExceeded3days()
        {
            try
            {
             var querable = appDbContext.UserCourses.AsEnumerable();

               
            var Exdd3DaysCourse = querable.Where(
                y=>y.isPaid==false && 
            (DateTime.Now-y.EnrollmentDate).TotalDays  > 3  ).ToList();
               
            await Task.Delay(10);
            appDbContext.UserCourses.RemoveRange(Exdd3DaysCourse);
            return true;
            }
            catch (Exception e)
            {

                return false;
            }
        

        }

        public bool UpdateUserCoursePaymentStatus(string UserId,int CourseId,bool NewState) 
        {
            try
            {
  var uc=   this.appDbContext.UserCourses.Where(y => y.CourseId == CourseId && y.UserId == UserId).AsTracking().FirstOrDefault();
            if (uc!=null && uc.isPaid != NewState)
            {
                uc.isPaid = NewState;

                    
            }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
       


        }

        public async Task<bool> UpdateUserCourseResult(int courseId, string userId, int grade)
        {
            try
            {
                var obj=await appDbContext.UserCourses.AsQueryable().AsTracking()
                   .Where(y => y.CourseId == courseId && y.UserId == userId).Include(y=>y.course).FirstAsync();

                if (grade < obj.course.FinalGrade&&grade>=0) 
                {

                    obj.Grade = grade;

                    if (grade >= (obj.course.FinalGrade / 2))
                    {
                        //passed
                        obj.StateId = 3;
                    }
                    else {
                        //failed
                        obj.StateId = 5;


                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
