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

        /// <summary>
        /// generic method for getting List of passed T with pagination
        /// </summary>
        /// <typeparam name="T"> T is generic class represent a class which has Dbset </typeparam>
        /// <returns> List of T </returns>
        public List<T> GetPaginatedList<T>(int pageSize=5,int currentPage=1) where T : class
        {


            var genericlist = appContext.Set<T>()
                .Skip(pageSize*(currentPage-1))
                .Take(pageSize)
                .ToList();
        
            return genericlist;
        }

    }
}
