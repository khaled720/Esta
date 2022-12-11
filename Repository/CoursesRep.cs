
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class CoursesRep : ICoursesRep
    {
        private readonly AppDbContext appContext;

        public CoursesRep( AppDbContext appContext)
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

        public async Task<bool> EditCourse(Course UpdatedCourse)
        {

            try
            {
                var DbCourse =await this.GetCourse(UpdatedCourse.Id);
                DbCourse.StartDate = UpdatedCourse.StartDate;
                DbCourse.LevelId=UpdatedCourse.LevelId;
                DbCourse.FinalGrade=UpdatedCourse.FinalGrade;
                DbCourse.Title=UpdatedCourse.Title;
                DbCourse.Price=UpdatedCourse.Price;
                DbCourse.PaymentLink = UpdatedCourse.PaymentLink;

                this.appContext.SaveChanges();

                //appContext.Entry<Course>(DbCourse).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await appContext.Courses.ToListAsync();
        }

        public async Task<Course> GetCourse(int id)
        {
            return await appContext.Courses.Where(y => y.Id == id).FirstOrDefaultAsync();
        }

     
    }
}
