using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class BannerRep : IBannerRep
    {
        private readonly AppDbContext _context;
        public BannerRep(AppDbContext context)
        {
            _context = context;
        }

        public List<HomeBanner> GetAllBanners()
        {
            return _context.HomeBanners.OrderBy(x => x.Type).ToList();
        }

        public HomeBanner? GetBanner(int Id)
        {
            return _context.HomeBanners.Where(x => x.Type == Id).FirstOrDefault();
        }

        public void InsertBanner(HomeBanner banner)
        {
            _context.HomeBanners.Add(banner);
        }
    }
}
