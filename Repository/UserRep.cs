using System.Runtime.CompilerServices;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
	public class UserRep : IUserRep
	{

        private readonly AppDbContext appContext;

        public UserRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<bool> EnrollCourse(int StateId,int CourseId,string UserId,bool isPaymentCompleted)
		{
         UserCourse userCourse = new UserCourse();
            userCourse.isPaid = isPaymentCompleted;
            userCourse.CourseId=CourseId;
           // userCourse.UserId=UserId;
            userCourse.StateId = StateId;


            await appContext.AddAsync<UserCourse>(userCourse);
            return true;
		}

        public async Task<IEnumerable<UserCourse>> GetMyCourses(string UserId)
        {
            //where ..
            return await appContext.UserCourses.Include(y => y.state).Include(y=>y.course).ToListAsync();
        }
    }
}
