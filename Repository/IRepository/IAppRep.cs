using System;
using ESTA.Areas.Payment.Repository;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUnitOfWork
    {

        ICertifiedMembersRep CertifiedMempersRep { get; }
        IRefundRep RefundRep { get; }
        IConstantsRep ConstantsRep { get; }
        ICourseOrders CourseOrdersRep { get; }
         ICoursePayments CoursePaymentsRep { get; }

        IMempershipOrders MempershipOrdersRep { get; }
        IMempershipPayments MempershipPaymentsRep { get; }
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
