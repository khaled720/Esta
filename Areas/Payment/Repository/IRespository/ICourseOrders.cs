using ESTA.Areas.Payment.Models;

namespace ESTA.Areas.Payment.Repository.IRespository
{
    public interface ICourseOrders
    {
        Task<bool> SavePrepareOrder(CourseOrder courseOrder);
        bool UpdatePrepareOrder(CourseOrder courseOrder);
        Task<bool> PostOrder(CourseOrder courseOrder);
        Task<List<CourseOrder>> GetCoursesOrders();
        Task<List<CourseOrder>> GetUnpaidCoursesFawryOrders();
        Task<CourseOrder> GetOrder(int orderId);
        Task<CourseOrder> GetOrderByNumber(string ordernumber, string userId);
        CourseOrder? GetUserCourseOrderNumber(int courseId, string userId);
        Task<int> GetMaxId();
        Task<int> GetOrdersLength();

    }
}
