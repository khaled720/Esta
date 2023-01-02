using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUserAnswerRep
    {


        Task<bool> AddAnswers(List<UserAnswer> answers);

    }
}
