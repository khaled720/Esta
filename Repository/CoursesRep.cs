﻿
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public bool DeleteCourse(int id)
        {
            try
            {
                appContext.Courses.Remove(appContext.Courses.First(y=>y.Id==id));
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
                DbCourse.Description=UpdatedCourse.Description;
                DbCourse.DescriptionAr=UpdatedCourse.DescriptionAr;
                DbCourse.TitleAr=UpdatedCourse.TitleAr;
                DbCourse.PhotoPath=UpdatedCourse.PhotoPath;
                

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
            return await appContext.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetCourse(int id)
        {

            return await appContext.Courses.AsNoTracking().Include(y=>y.level)
                .Where(y => y.Id == id).FirstAsync();

        }

        public async Task<List<UserCourse>> GetEnrolledUsersInCourse(int id)
        {
            return await appContext.UserCourses.AsNoTracking().Where(y => y.CourseId == id).Include(y => y.user).ToListAsync();
        }

        public async Task<string> SearchForCourse(string Name)
        {
         var courses= await appContext.Courses.Where(y=>y.Title.Contains(Name)).ToListAsync();
            
     return JsonSerializer.Serialize(courses);

        }
    }
}
