using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IBannerRep
    {
        public void InsertBanner(HomeBanner banner);
        public HomeBanner? GetBanner(int Id);
        public List<HomeBanner> GetAllBanners();
    }
}
