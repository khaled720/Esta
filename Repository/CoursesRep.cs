
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

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await appContext.Courses.ToListAsync();
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

     
    }
}
