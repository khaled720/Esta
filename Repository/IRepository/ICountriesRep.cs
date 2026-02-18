using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ICountriesRep
    {
        List<Countries> GetCountries();
        Countries? GetCountry(int id);
        Countries? GetCountry(string NameEn);
    }
}
