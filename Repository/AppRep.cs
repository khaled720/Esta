using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class AppRep : IAppRep
    {
        private readonly AppDbContext appContext;

        public AppRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
        public ICoursesRep CoursesRep => new CoursesRep(appContext);

        public ILevelRep LevelRep =>  new LevelsRep(appContext);

        public IUserRep UserRep =>  new UserRep(appContext);

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
