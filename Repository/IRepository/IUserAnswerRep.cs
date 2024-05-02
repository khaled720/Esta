using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUserAnswerRep
    {


        Task<bool> AddAnswers(List<UserAnswer> answers);
        Task<List<UserAnswer>> GetUsersAnswers(string UserId);

    }
}
