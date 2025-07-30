using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class LogosRep : ILogosRep
    {
        private readonly AppDbContext context;

        public LogosRep(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteLogo(int id)
        {
            var logo = context.Logos.Find(id);
            if (logo != null)
            {
                context.Logos.Remove(logo);
            }
        }

        public List<Logo> GetAllLogos()
        {
            return context.Logos.ToList();
        }

        public void UploadLogo(Logo logo)
        {
            if (logo != null)
            {
                context.Logos.Add(logo);
            }
        }
    }
}
