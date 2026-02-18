using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class CountriesRep : ICountriesRep
    {
        private readonly AppDbContext _context;
        public CountriesRep(AppDbContext context)
        {
            _context = context;
        }

        public List<Countries> GetCountries()
        {
            try
            {
                return _context.Countries.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Countries? GetCountry(int id)
        {
            try
            {
                return _context.Countries.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Countries? GetCountry(string NameEn)
        {
            try
            {

                return _context.Countries.Where(x => x.NameEn == NameEn).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
