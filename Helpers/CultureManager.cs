using ESTA.Models;
using ESTA.Repository.IRepository;
using System.Globalization;

namespace ESTA.Helpers
{
    public class CultureManager
    {
        private readonly IUnitOfWork uow;

        public CultureManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public IEnumerable<Countries> GetCountries()
        {
            return uow.CountriesRep.GetCountries(); 
        }
    }
}
