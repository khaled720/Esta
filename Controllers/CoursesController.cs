using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{

    public class CoursesController : Controller
    {
        private readonly IAppRep appRep;

        public CoursesController( IAppRep appRep)
        {
            this.appRep = appRep;
        }
        public async Task<IActionResult> Index()
        {
        List<Course> courses= (List<Course>)await appRep.CoursesRep.GetAllCourses();
            return View(courses);
        }

        [HttpGet]
        public  IActionResult AddCourse()
        {
         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(Course course)
        {
            var isAdded = await appRep.CoursesRep.AddCourse(course);
            if (isAdded)
            {
                return RedirectToAction("Index");

            }
            else {
            return View(course);
            }

        }


    }
}
