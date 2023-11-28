using ESTA.Areas.Payment.Models;

namespace ESTA.Areas.Payment.Repository.IRespository
{
    public interface ICoursePayments
    {

        Task<bool> SaveGetOrder(CoursePayment coursePayment);

       Task<List<CoursePayment>> GetCoursesPayments();

    }
}
