using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IAppRep
    {


        ICoursesRep CoursesRep { get; }
        ILevelRep LevelRep { get; }
        IUserRep UserRep { get; }
        IForumRepository ForumRep { get; }
        Task<bool> SaveChangesAsync();
    }
}
