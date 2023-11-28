using ESTA.Models;

namespace ESTA.Repository.IRepository
{
	public interface IUserRep
	{


        Task<string> GetAdminUserEmail();
        public Task<bool> EnrollCourse(int StateId, int CourseId, string UserId, bool isPaymentCompleted);
        public Task<IEnumerable<UserCourse>> GetMyCourses(string id);

        public Task<IEnumerable<User>> GetAllUsers();

        public Task<User> GetUser(string userId);

        public Task<bool> PayMempership(string userId);


        public Task<bool>  RevokeMempershipPayment(string userId);


        public Task<bool> IsUserMempershipPaid(string userId);

        public Task<bool> DeleteUser(string id);
        public Task<bool> EditUserApproval(string id,bool isApproved);

        public Task<bool> EditUserEmailConfirmationApproval(string id, bool isConfirmed);

Task<bool>   UpdateUserLevel(string userId);
        Task<bool> IsForeignUser(string UserId);
    }
}
