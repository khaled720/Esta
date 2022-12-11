using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IAppRep
    {


        ICoursesRep CoursesRep { get; }
        ILevelRep LevelRep { get; }
        Task<bool> SaveAsync();
    }
}
