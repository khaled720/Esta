using ESTA.Models;

namespace ESTA.ViewModels
{
    public class HomeIndexViewModel
    {


        public string?  About  { get; set; }
        public string? Mission { get; set; }
        public string?  Vission { get; set; }
        public List<Course> UpcomingCourse { get; set; }
        public List<Logo> LogosList { get; set; }
        public List<ViewBanner> BannerList { get; set; }=new List<ViewBanner>();
    }
}
