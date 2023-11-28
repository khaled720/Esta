using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
using ESTA.Areas.Admin.Models;
using ESTA.Areas.Admin.ViewModels;
using ESTA.Models;
using ESTA.Repository.IRepository;

using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class CoursesRep : ICoursesRep
    {
        private readonly AppDbContext appContext;

        public CoursesRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<bool> AddCourse(Course course)
        {
            try
            {
                await appContext.Courses.AddAsync(course);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCourse(int id)
        {
            try
            {
                appContext.Courses.Remove(appContext.Courses.First(y => y.Id == id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditCourse(Course UpdatedCourse)
        {
            try
            {
                var DbCourse = await this.GetCourse(UpdatedCourse.Id);
                DbCourse.StartDate = UpdatedCourse.StartDate;
                DbCourse.LevelId = UpdatedCourse.LevelId;
                DbCourse.FinalGrade = UpdatedCourse.FinalGrade;
                DbCourse.Title = UpdatedCourse.Title;
                DbCourse.Price = UpdatedCourse.Price;
           //  DbCourse.PaymentLink = UpdatedCourse.PaymentLink;
                DbCourse.Description = UpdatedCourse.Description;
                DbCourse.DescriptionAr = UpdatedCourse.DescriptionAr;
                DbCourse.TitleAr = UpdatedCourse.TitleAr;
                DbCourse.PhotoPath = UpdatedCourse.PhotoPath;
                if (UpdatedCourse.MaxAllowedMembersCount > DbCourse.MaxAllowedMembersCount) {
                    DbCourse.MaxAllowedMembersCount = UpdatedCourse.MaxAllowedMembersCount;
                }
                this.appContext.SaveChanges();

                //appContext.Entry<Course>(DbCourse).State = EntityState.Modified;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            try
            {
       return await appContext.Courses.AsNoTracking().ToListAsync();
        
     
            }
            catch (Exception)
            {

          return Enumerable.Empty<Course>();
            }
         
        }

    


        public async Task<IEnumerable<Course>> GetAllCoursesByLevel(int LevelId)
        {
            return await appContext.Courses
                .Where(y => y.LevelId == LevelId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCetaCourses()
        {
            return await appContext.Courses.Where(y => y.LevelId < 4).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllOtherCourses()
        {
            return await appContext.Courses.Where(y => y.LevelId == 4).AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetCourse(int id)
        {
            try
            {
      return await appContext.Courses
                //.AsNoTracking()
                .Include(y => y.level)
                .Where(y => y.Id == id)
                .FirstAsync();
            }
            catch (Exception)
            {
                return null;
            }
      
        }

        public async Task<List<UserCourse>> GetEnrolledUsersInCourse(int id)
        {
            return await appContext.UserCourses
                .AsQueryable()
                .AsNoTracking()
                .Where(y => y.CourseId == id)
                .Include(y => y.user)
                .Include(y=>y.state)
                .ToListAsync();
        }

        public async Task<string> SearchForCourse(string Name)
        {
            var courses = await appContext.Courses.Where(y => y.Title.Contains(Name)).ToListAsync();

            return JsonSerializer.Serialize(courses);
        }

        public async Task<IEnumerable<User>> GetAllCetaHolders()
        {
            var CetaHolders = new List<User>();
            var CompletedCetaCourses = await appContext.UserCourses
                .Include(y => y.course)
                .Include(y=>y.user)
                .Where(y => y.StateId == 3)
                .Where(y=>y.course.LevelId!=4)
                .ToListAsync();

            var UniqueUsers = CompletedCetaCourses.DistinctBy(y => y.UserId);

            foreach (var item in UniqueUsers)
            {
                if (CompletedCetaCourses.Where(y => y.UserId == item.UserId).ToList().Count() == 3)
                {
                    //ceta holder user
                    CetaHolders.Add(item.user);
                }
            }
            return CetaHolders;
        }

        public async Task<Course> GetUpcomingCourse()
        {

            try
            {
            
                var course =await appContext.Courses.OrderBy(y=>y.StartDate)
                    .Where(y => y.StartDate > DateTime.Now).FirstOrDefaultAsync();
                return course;

            }
            catch (Exception)
            {
                return new Course();
            }


        }

        public async Task<List<Course>> SearchCoursesByName(string query)
        {

            try
            {
             return   await appContext.Courses.AsQueryable().Where(y => y.Title.Contains(query)).ToListAsync();
               
            }
            catch (Exception)
            {
                return new List<Course>();
            }


        }

        public async Task<bool> IsCourseEnrolledByUser(int courseId, string userId)
        {
            try
            {
             return await   appContext.UserCourses.Where(y=>y.CourseId==courseId&&y.UserId==userId).AnyAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        



        public async Task<int> GetEnrolledUsersInCourseLength(int courseId)
        {

            var usersNumber= await appContext.UserCourses
            .AsNoTracking()
            .Where(y => y.CourseId == courseId).CountAsync();
            return usersNumber;
        }

        public async Task<bool> UpdateCourseState(int courseId, string userId, int StateId)
        {

            try
            {

         var usercourse = await appContext.UserCourses
                .Where(y => y.CourseId == courseId && y.UserId == userId).AsTracking()
                .FirstOrDefaultAsync();
                usercourse.StateId = StateId;

                return true;
        
            }catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> AddPrerequisiteCourses(List<PrerequisiteCourse> prerequisiteCourses)
        {
            try
            {

                appContext.PrerequisiteCourses.RemoveRange(
                    appContext.PrerequisiteCourses.Where(y => y.MainCourseId == prerequisiteCourses[0].MainCourseId).ToList()
                    );
                
          await      appContext.PrerequisiteCourses.AddRangeAsync(prerequisiteCourses);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> IsPrerequisiteCourse(int MainCourseId, int PreCourseId)
        {
            try
            {


                return await appContext.PrerequisiteCourses.Where(y => y.MainCourseId == MainCourseId 
                && y.PrerequisiteCourseId == PreCourseId).AnyAsync();
               

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<PrerequisiteCourse>> GetPrerequisiteCourses(int MainCourseId)
        {
            try
            {


                return await appContext.PrerequisiteCourses.Where(y => y.MainCourseId == MainCourseId).Include(y=>y.prerequisiteCourse).ToListAsync();


            }
            catch (Exception)
            {

                return new List<PrerequisiteCourse>();
            }
        }
    }
}
