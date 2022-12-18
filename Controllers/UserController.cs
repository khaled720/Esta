using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;

        public UserController(IAppRep appRep, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Profile()
        {
            //     HttpContext.Session.SetString();
          var user =await  this.userManager.GetUserAsync(HttpContext.User);
           
            
            
            return View();
        }


        public async Task<IActionResult> Courses()
        {
            // user corses shuild be lodd her
            //User Id Must be Dynamic
      var courses=    await  appRep.UserRep.GetMyCourses("0fc3d362-4672-46fb-8876-fb79a32257ca");

            return View();
        }

        public async Task<IActionResult> EnrollCourse(int id)
        {

          await  appRep.UserRep.EnrollCourse(1, id, "0fc3d362-4672-46fb-8876-fb79a32257ca", true);
           await appRep.SaveAsync();
            return View();

        }
        public IActionResult CourseEnrolled()
        {
           


            return View();
        }




    }
}
