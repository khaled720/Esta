using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface ILogosRep
    {
        public List<Logo> GetAllLogos();
        public void UploadLogo(Logo logo);
        public void DeleteLogo(int id);
    }
}
