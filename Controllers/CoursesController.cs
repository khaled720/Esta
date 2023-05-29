using System.Text.Encodings.Web;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESTA.Controllers
{
    //  [Route("Admin/{controller}/{action=Index}/{id?}")]
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;

        public CoursesController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
        }

      

        public async Task<IActionResult> OtherCourses()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllOtherCourses();
            return View(courses);
        }

        public async Task<IActionResult> CetaCourses()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCetaCourses();
            return View(courses);
        }

        public async Task<IActionResult> CetaHolders()
        {
            List<User> users = (List<User>)await appRep.CoursesRep.GetAllCetaHolders();
            return View(users);
        }

        public async Task<IActionResult> CourseDetails(int id)
        {
            var course = await appRep.CoursesRep.GetCourse(id);

            return View(course);
        }

   
    }

    public class EmailClass
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }

 
}
