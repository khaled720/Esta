using System.Diagnostics;
using System.Runtime.CompilerServices;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                var user = await appContext.Users.AsTracking().Where(y => y.Id == id).FirstAsync();

                user.IsDeleted = true;
                user.DeletionTime = DateTime.Now;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> EditUserApproval(string id, bool isApproved)
        {
            try
            {
                var user = await appContext.Users.AsTracking().Where(y => y.Id == id).FirstAsync();

                user.IsApproved = isApproved;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public async Task<bool> EditUserEmailConfirmationApproval(string id, bool isConfirmed)
        {
            try
            {
                var user = await appContext.Users.AsTracking().Where(y => y.Id == id).FirstAsync();

                user.EmailConfirmed = isConfirmed;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //public Task<bool> EditUserLevel(int newLevel, string userId)
        //{
            
        //    return true;





        //}

        public async Task<bool> EnrollCourse(int StateId,int CourseId,string UserId,bool isPaymentCompleted)
		{

            try
            {


           var isRegistredBefore= 
                    appContext.UserCourses.Where(y => y.UserId == UserId && y.CourseId == CourseId).Any();
                if (isRegistredBefore)
                {    //course  registred befor
                    
                    var usercourse= appContext.UserCourses.Where(y => y.UserId == UserId && 
           y.CourseId == CourseId).AsTracking().FirstOrDefault();
                    if (usercourse.StateId == 4) 
                    {

                        usercourse.StateId = StateId;
                        usercourse.isPaid = isPaymentCompleted;
                        await appContext.SaveChangesAsync();

                    }



                }
                else
                {
                    //course not registred befor
            UserCourse userCourse = new UserCourse();
            userCourse.isPaid = isPaymentCompleted;
            userCourse.CourseId=CourseId;
            userCourse.UserId=UserId;
            userCourse.StateId = StateId; 
                await appContext.AddAsync<UserCourse>(userCourse);

                }
        
            return true;
            }
            catch (Exception)
            {
                return false;
            }
           


           
		}

        public async Task<string> GetAdminUserEmail()
        {
       var adminrole=     appContext.Roles.Where(y => y.NormalizedName == "ADMIN").First();
          var userole= appContext.UserRoles.Where(y=>y.RoleId==adminrole.Id).First();
 var user= await       appContext.Users.FindAsync(userole.UserId);
            return user.Email;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await appContext.Users.Where(y=>y.IsDeleted!=true).ToListAsync();
        }

        public async Task<IEnumerable<UserCourse>> GetMyCourses(string UserId)
        {
            
            //where ..........

            return await appContext.UserCourses.AsNoTracking()
                .Include(y => y.state).Include(y => y.course).Where(y=>y.UserId==UserId).ToListAsync();
        }

        public async Task<User> GetUser(string userId)
        {
            try
            {

                return await appContext.Users.FirstAsync(y=>y.Id==userId);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> IsForeignUser(string UserId)
        {

            try
            {

                var userq = appContext.Users.AsQueryable();
                var usr = await userq.Where(y => y.Id == UserId).FirstAsync();
                switch (usr.Country)
                {
                    case "Egypt":
                        return false;

                    default:
                        return true;
                  
                }

            }

            catch (Exception)
            {

                return true;
            }
        }

        public async Task<bool> IsUserMempershipPaid(string userId)
        {
            try
            {

                //Stopwatch stopwatch=new Stopwatch();
                //Stopwatch stopwatch1 = new Stopwatch();

                //stopwatch.Start();
                //var user4 = appContext.Courses.AsQueryable();
                //var usruu = await user4.Where(y => y.Id == 26).FirstAsync();
                //stopwatch.Stop();

                //stopwatch1.Start();
                //var user6 = appContext.Courses.AsEnumerable();
                //var usrii =  user6.Where(y => y.Id == 26).First();
                //stopwatch1.Stop();


                var userq = appContext.Users.AsQueryable();
                var usr = await userq.Where(y => y.Id == userId).FirstAsync();

                return usr.IsMempershipPaid;
            }

            catch (Exception)
            {

                return false;
            }
        }

        public async  Task<bool> PayMempership(string userId)
        {
         
            try
            {
              var user=  await appContext.Users.Where(y => y.Id == userId).FirstAsync();
                user.IsMempershipPaid = true;
                appContext.Users.Update(user);
            
            }
            catch (Exception)
            {

                throw;
            }
           
return  await  Task.FromResult(true);
        }

        public async Task<bool> RevokeMempershipPayment(string userId)
        {
            try
            {
                var user = await appContext.Users.Where(y => y.Id == userId).FirstAsync();
                user.IsMempershipPaid = false;
                appContext.Users.Update(user);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateUserLevel(string userId)
        {
            try
            {
                var UserCourses = await appContext.UserCourses
                                  .Where(y => y.UserId == userId && y.StateId != 4)
                                  .Include(y => y.course).ToListAsync();

                int? MaxCourseLevelID;


                try
                {

                    MaxCourseLevelID = UserCourses.Where(y => y.course.LevelId != 4)
                    .Max(y => y.course.LevelId);

                }
                catch (Exception)
                {

                    MaxCourseLevelID = 4;
                }
                if (MaxCourseLevelID != null)
                {
                    var user = appContext.Users.Find(userId);
                    if (user != null)
                    {
                        user.LevelId = (int)MaxCourseLevelID;
                    }


                }
                else
                {
                    var user = appContext.Users.Find(userId);
                    if (user != null)
                    {
                        user.LevelId = 4;
                    }
                }



                return true;
            }
            catch (Exception e) { return false; }

        }


    }
}
