using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Areas.Payment.Repository
{
    public class CoursePaymentRep : ICoursePayments
    {
        private readonly AppDbContext appDbContext;

        public CoursePaymentRep(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<CoursePayment>> GetCoursesPayments()
        {
            try
            {

                return await appDbContext.CoursesPayments
                    .Include(y=>y.Course).Include(y=>y.User).ToListAsync();
            }
            catch (Exception)
            {

                return  new List<CoursePayment>();
            }
        }

        public Task<bool> SaveGetOrder(CoursePayment coursePayment)
        {
       appDbContext.CoursesPayments.Add(coursePayment);
            return Task.FromResult(true);

        }
    }
}
