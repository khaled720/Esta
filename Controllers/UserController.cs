using System.Security.Claims;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;

        public UserController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Profile()
        {

            await appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            
            return View();
        }

        public async Task<IActionResult> Courses()
        {
            // user corses shuild be lodd her
            //User Id Must be Dynamic
      var courses=    await  appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(courses);
        }

        
        public async Task<IActionResult> EnrollCourse(int Id)
        {

            if (User!=null&&User.Identity.IsAuthenticated)
            {
                await appRep.UserRep.EnrollCourse(1, Id, User.FindFirstValue(ClaimTypes.NameIdentifier), false);
                await appRep.SaveChangesAsync();
                return View();
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
        public async Task<IActionResult> PayEnrollCourse(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                await appRep.UserRep.EnrollCourse(1, Id, User.FindFirstValue(ClaimTypes.NameIdentifier), true);
                await appRep.SaveChangesAsync();
                return RedirectToAction("Courses");
            }
            else
            {
                return Redirect("/Account/Login");
            }
         

        }
        public IActionResult CourseEnrolled()
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
