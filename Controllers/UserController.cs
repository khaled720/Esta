using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;

        public UserController(IAppRep appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;

        }


        public IActionResult Profile()
        {
       //     HttpContext.Session.SetString();
            
            return View();
        }


        public IActionResult Courses()
        {
            // user corses shuild be lodd her
       

            return View();
        }



    }
}
