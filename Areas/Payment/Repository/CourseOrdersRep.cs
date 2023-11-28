using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Areas.Payment.Repository
{
    public class CourseOrdersRep : ICourseOrders
    {
        private readonly AppDbContext appContext;

        public CourseOrdersRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
     public async Task<bool> SavePrepareOrder(CourseOrder courseOrder)
        {
          await   appContext.CoursesOrders.AddAsync(courseOrder);
            return true;

        }

        public bool  UpdatePrepareOrder(CourseOrder courseOrder)
        {
             appContext.CoursesOrders.Update(courseOrder);

            return true;
        }
        public async Task<CourseOrder> GetOrder(int orderId)
        {
  return       await appContext.CoursesOrders.FindAsync(orderId)??new CourseOrder();
        }

        public Task<bool> PostOrder(CourseOrder courseOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetOrdersLength()
        {
            try
            {
   return await  appContext.CoursesOrders.CountAsync();
            }
            catch (Exception)
            {
                return 0;
            
            }
   
        }

        public async Task<List<CourseOrder>> GetCoursesOrders()
        {
            try
            {
                return await appContext.CoursesOrders.ToListAsync();    
            }
            catch (Exception)
            {

       return new List<CourseOrder>();
            }
        }

        public async Task<int> GetMaxId()
        {
            try
            {
                var result =await  appContext.CoursesOrders.MaxAsync(y => y.Id);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CourseOrder> GetOrderByNumber(string ordernumber,string userId)
        {
            try
            {
       var courseOrder=         await appContext.CoursesOrders
                    .Where(y => y.OrderNumber == ordernumber&&y.UserId==userId).FirstOrDefaultAsync();
                return courseOrder;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
