using ESTA.Areas.Payment.Repository;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace ESTA.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appContext;
        private readonly UserManager<User> userManager;

        public UnitOfWork(AppDbContext appContext,UserManager<User> userManager)
        {
            this.appContext = appContext;
            this.userManager = userManager;
        }


        public IMempershipOrders MempershipOrdersRep => new MempershipOrdersRep(appContext);

        public IMempershipPayments MempershipPaymentsRep => new MempershipPaymentsRep(appContext);

        public ICoursesRep CoursesRep => new CoursesRep(appContext);

        public ILevelRep LevelRep =>  new LevelsRep(appContext);

        public IUserRep UserRep =>  new UserRep(appContext);

        public IQuestionRep QuestionRep => new QuestionRep(appContext);

        public IContentRep ContentRep =>  new ContentRep(appContext);

        public IDirectorRep DirectorRep => new DirectorRep(appContext);

        public IContactRep ContactRep => new ContactRep(appContext);

        public IUserAnswerRep UserAnswerRep => new UserAnswerRep(appContext);

        public IUsersCoursesRep UsersCoursesRep => new UsersCoursesRep(appContext,userManager);

        public IForumRepository ForumRep => new ForumRepository(appContext);
        public IEventsRepo EventRep => new EventRepo(appContext);

        public IUserImageRep ImageRep => new UserImageRep(appContext);

        public IModeratorRep ModeratorRep => new ModeratorRep(appContext);

        public IForumBannedUserRep ForumBannedUserRep => new ForumBannedUserRep(appContext);

        public ICourseOrders CourseOrdersRep => new CourseOrdersRep(appContext);

        public ICoursePayments CoursePaymentsRep => new CoursePaymentRep(appContext);

        public IConstantsRep ConstantsRep =>  new ConstantsRep(appContext);

        public IRefundRep RefundRep => new RefundRep(appContext);

        public ICertifiedMembersRep CertifiedMempersRep => new CertifiedMembersRep(appContext);

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
