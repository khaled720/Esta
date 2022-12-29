using System.Diagnostics;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork Uow;
        private readonly IWebHostEnvironment hostEnvironment;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork appRep, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.Uow = appRep;
            this.hostEnvironment = hostEnvironment;
            
        }

        public async Task<IActionResult> Index()
        {
   

            List<Course> courses = (List<Course>)await Uow.CoursesRep.GetAllCourses();
            return View(courses);
        }




        public async Task<IActionResult> CourseDetails(int id)
        {
           var course =await Uow.CoursesRep.GetCourse(id);


            return View(course);
        }












        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}