using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appContext;

        public UnitOfWork(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
        public ICoursesRep CoursesRep => new CoursesRep(appContext);

        public ILevelRep LevelRep =>  new LevelsRep(appContext);

        public IUserRep UserRep =>  new UserRep(appContext);

        public IQuestionRep QuestionRep => new QuestionRep(appContext);

        public IContentRep ContentRep =>  new ContentRep(appContext);

        public IDirectorRep DirectorRep => new DirectorRep(appContext);

        public IContactRep ContactRep => new ContactRep(appContext);

        public IUserAnswerRep UserAnswerRep => new UserAnswerRep(appContext);

        public IUsersCoursesRep UsersCoursesRep => new UsersCoursesRep(appContext);

        public IForumRepository ForumRep => new ForumRepository(appContext);
        public IEventsRepo EventRep => new EventRepo(appContext);

        public async Task<bool> SaveChangesAsync()
        {
         return await   this.appContext.SaveChangesAsync()>0;
        }

        public  void RollbackChangesAsync()
        {
 
                 this.appContext.ChangeTracker.Clear();
        }


    }
}
