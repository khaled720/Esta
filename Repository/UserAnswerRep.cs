using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<UserAnswer>> GetUsersAnswers(string UserId)
        {
            return await appContext.UserAnswers.Where(x => x.UserId == UserId)
                .Include(x => x.question).ToListAsync();
        }
    }
}
