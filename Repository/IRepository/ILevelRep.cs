using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ILevelRep
    {

        public Task<IEnumerable<Level>> GetAllLevels();
        public Level GetLevel(int id);

        public Task<bool> EditLevel(Level level);

        public Task<bool> AddLevel(Level level);
    }
}
