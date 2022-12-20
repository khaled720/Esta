using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ILevelRep
    {

        public Task<IEnumerable<Level>> GetAllLevels();
        public Course GetLevel(int id);

        public Task<bool> AddLevel(Level level);
    }
}
