using ESTA.Areas.Admin.ViewModels;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Banners")]
    public class BannersController : Controller
    {
        private readonly IUnitOfWork appRep;
        public BannersController(IUnitOfWork appRep)
        {
            this.appRep = appRep;
        }

        public IActionResult Index(int type, bool? edit)
        {
            HomeBanner? Banner = appRep.BannerRep.GetBanner(type);

            GetBanner? getBanner = null;

            if (Banner != null)
                getBanner = new()
                {
                    FilePath = Banner.FilePath,
                    Type = Banner.Type,
                    SloganAr = Banner.SloganAr,
                    SloganEn = Banner.SloganEn,
                    DetailsAr = Banner.DetailsAr,
                    DetailsEn = Banner.DetailsEn
                };

            //ViewBag.Edit = edit == null ? false : edit;
            ViewBag.Edit = edit;
            ViewBag.Type = type;

            return View(getBanner);
        }
        [HttpPost]
        public async Task<IActionResult> InsertBannerAsync(GetBanner GetBanner)
        {
            HomeBanner? Banner = appRep.BannerRep.GetBanner(GetBanner.Type);
            bool res;

            if (Banner == null)
            {
                Banner = new HomeBanner
                {
                    Type = GetBanner.Type,
                    FilePath = ImageHelper.UploadedFile(GetBanner.ImageFile, "Images/Banners"),
                    SloganAr = GetBanner.SloganAr,
                    SloganEn = GetBanner.SloganEn,
                    DetailsAr = GetBanner.DetailsAr,
                    DetailsEn = GetBanner.DetailsEn
                };

                appRep.BannerRep.InsertBanner(Banner);
                res = await appRep.SaveChangesAsync();
            }
            else
            {
                Banner.SloganAr = GetBanner.SloganAr;
                Banner.SloganEn = GetBanner.SloganEn;
                Banner.DetailsAr = GetBanner.DetailsAr;
                Banner.DetailsEn = GetBanner.DetailsEn;

                string OldPhoto = string.Empty;

                if (GetBanner.ImageFile != null)
                {
                    OldPhoto = Banner.FilePath;

                    Banner.FilePath = ImageHelper.UploadedFile(GetBanner.ImageFile, "Images/Banners");
                }

                res = await appRep.SaveChangesAsync();
                if (res && OldPhoto != string.Empty)
                {
                    ImageHelper.DeleteFile("Images/Banners", OldPhoto);
                }
            }

            return RedirectToAction("Index", new { type = GetBanner.Type, edit = res });
        }
    }
}
