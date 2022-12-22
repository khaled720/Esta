using ESTA.Models;
using System;

namespace ESTA.Repository.IRepository
{
    public interface IQuestionRep
    {


        public  Task<IEnumerable<Question>> GetAllQuestions();
    }
}
