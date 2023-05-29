using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;

        public HomeController(
            IUnitOfWork appRep,
            IWebHostEnvironment hostEnvironment,
            UserManager<User> userManager
        )
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
          //  var x = DateTime.UtcNow;

            return View();
        }

    

    }
}
