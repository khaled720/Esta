using ESTA.Areas.Admin.ViewModels;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Logos")]
    public class LogosController : Controller
    {
        private readonly IUnitOfWork appRep;

        public LogosController(IUnitOfWork appRep)
        {
            this.appRep = appRep;
        }
        public IActionResult Index()
        {
            //var logos = appRep.LogosRep.GetAllLogos();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadLogo(LogosVM file)
        {
            if (file != null && file.ImageFile.Length > 0)
            {
                Logo logo = new Logo
                {
                    ImagePath = ImageHelper.UploadedFile(file.ImageFile, "images/Logos"),
                };
                appRep.LogosRep.UploadLogo(logo);
            }

            var res = await appRep.SaveChangesAsync();

            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteLogoAsync(int id)
        {
            appRep.LogosRep.DeleteLogo(id);
            var res = await appRep.SaveChangesAsync();

            return View("Index");
        }
        [HttpGet]
        public IActionResult GetLogos()
        {
            var logo = appRep.LogosRep.GetAllLogos();

            return View("_GetLogos", logo);
        }
    }
}
