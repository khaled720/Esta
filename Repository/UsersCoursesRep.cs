using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class UsersCoursesRep : IUsersCoursesRep
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<User> userManager;

        public UsersCoursesRep(AppDbContext appDbContext,UserManager<User> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }

        public bool AddUsertoCourseAsCompleted(int courseId, string userId)
        {
            try
            {
               appDbContext.UserCourses.Add(
                    new UserCourse { CourseId = courseId, UserId = userId,
                    isPaid = true,StateId=3,Grade=100 });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteUserFromCourse(int cId, string uid)
        {
            try
            {
                var usercourse = await appDbContext.UserCourses.Where(y => y.UserId == uid && y.CourseId == cId).FirstAsync();
                if (usercourse!=null&&usercourse.isPaid==false&&usercourse.Grade==0) 
                {
                     appDbContext.Remove<UserCourse>(usercourse);
                    return true;
                }

                return false;
            }
            catch (Exception e )
            {
                return false;
            }
        }

        public List<UserCourse>     GetAllUsersEnrolledinCourse(int courseId)
            {

      return     appDbContext.UserCourses.AsQueryable().Where(y => y.CourseId == courseId).Include(y => y.user).ToList();


}

        public List<User> GetAllUsersNotEnrolledinCourse(int courseId)
        {


            var notUsers = new List<User>();
            var UsersRegistredInCourse = new List<User>();
            var allusers = new List<User>();
           // var wherecon;
           allusers = appDbContext.Users.ToList();
            foreach (var user in allusers)
            {
                if (userManager.IsInRoleAsync(user, "User").GetAwaiter().GetResult()) 
                {

                    notUsers.Add(user);
                }
            }
            
            
            
            var us = appDbContext.UserCourses.AsQueryable().Where(y => y.CourseId == courseId).ToList();

            

            foreach (var usercourse in us)
            {
               
                foreach (var user in notUsers) 
                {

                    if (usercourse.UserId == user.Id) { UsersRegistredInCourse.Add(user); break; }
                }

            }

            return notUsers.Except(UsersRegistredInCourse).ToList();

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
                var SuccessGrade = appDbContext.Courses.AsQueryable().Where(y=>y.Id==courseId).First().SuccessPersentage;
                if (grade < obj.course.FinalGrade&&grade>=0) 
                {

                    obj.Grade = grade;

                    if (grade >= (SuccessGrade*(obj.course.FinalGrade / 100)))
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
