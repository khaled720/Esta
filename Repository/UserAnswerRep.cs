using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class UserAnswerRep : IUserAnswerRep
    {
        private readonly AppDbContext appContext;

        public UserAnswerRep( AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<bool> AddAnswers(List<UserAnswer> answers)
        {
          
            await  appContext.UserAnswers.AddRangeAsync(answers);
            return true;

        }
    }
}
