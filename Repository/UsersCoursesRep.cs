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





    }
}
