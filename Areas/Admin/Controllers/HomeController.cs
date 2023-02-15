using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;

        public HomeController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }

        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> UsersApproval()
        {
            var users = await appRep.UserRep.GetAllUsers();
            return View(users);
        }
        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> EditApproval(string id, bool isApproved)
        {
            await appRep.UserRep.EditUserApproval(id, isApproved);
            await appRep.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
