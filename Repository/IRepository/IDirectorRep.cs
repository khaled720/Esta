using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IDirectorRep
    {

        public Task<IEnumerable<Director>> GetAllDirectors();
        public Task<Director> GetDirector(int id);

        public Task<bool> AddDirector(Director director);

        public Task<bool> EditDirector(Director director);
        public bool DeleteDirector(int id);
    }
}
