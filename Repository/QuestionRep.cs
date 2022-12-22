using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class QuestionRep : IQuestionRep
    {
        private readonly AppDbContext appContext;

        public QuestionRep(AppDbContext appContext)
        {
            this.appContext = appContext;

        }
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await appContext.Questions.AsNoTracking().ToListAsync();
        }
    }
}
