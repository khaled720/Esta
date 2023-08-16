using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUnitOfWork
    {


        ICoursesRep CoursesRep { get; }
        ILevelRep LevelRep { get; }
        IUserRep UserRep { get; }
        IDirectorRep DirectorRep { get; }
        IContentRep ContentRep { get; }
        IQuestionRep QuestionRep { get; }
        IUserAnswerRep UserAnswerRep { get; }
        IUsersCoursesRep UsersCoursesRep { get; }
        IContactRep ContactRep { get; }
        IForumRepository ForumRep { get; }
        IEventsRepo EventRep { get; }
        IUserImageRep ImageRep { get; }
        IModeratorRep ModeratorRep { get; }
        IForumBannedUserRep ForumBannedUserRep { get; }
        Task<bool> SaveChangesAsync();
        public List<T> GetPaginatedList<T>(int pageSize = 5, int currentPage = 1) where T : class;


    }
}
