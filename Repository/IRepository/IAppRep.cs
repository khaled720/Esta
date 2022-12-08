using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IAppRep
    {


        ICoursesRep CoursesRep { get; }

        Task<bool> SaveAsync();
    }
}
