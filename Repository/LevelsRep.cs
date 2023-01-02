using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class LevelsRep : ILevelRep
    {
        private readonly AppDbContext appContext;

        public LevelsRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
        public async Task<bool> AddLevel(Level level)
        {
            try
            {
                await appContext.Levels.AddAsync(level);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditLevel(Level level)
        {
            try
            {
            var lvl=await appContext.Levels.FirstAsync(y=>y.Id==level.Id);
                lvl.TypeName=level.TypeName;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Level>> GetAllLevels()
        {
            return await appContext.Levels.ToListAsync();
        }

        public Course GetLevel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
