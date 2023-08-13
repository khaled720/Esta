using ESTA.Models;

namespace ESTA.Repository.IRepository
{
	public interface IUserRep
	{


       
        public Task<bool> EnrollCourse(int StateId, int CourseId, string UserId, bool isPaymentCompleted);
        public Task<IEnumerable<UserCourse>> GetMyCourses(string id);

        public Task<IEnumerable<User>> GetAllUsers();

        public Task<User> GetUser(string userId);


        public Task<bool> DeleteUser(string id);
        public Task<bool> EditUserApproval(string id,bool isApproved);

        public Task<bool> EditUserEmailConfirmationApproval(string id, bool isConfirmed);
    }
}
