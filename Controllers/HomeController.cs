using System.Diagnostics;
using AutoMapper;
using ESTA.Migrations;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IMapper mapper;

        public HomeController(IMapper mapper, ILogger<HomeController> logger, IAppRep appRep, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {


            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCourses();
            return View(courses);
        }




        public async Task<IActionResult> CourseDetails(int id)
        {
            var course = await appRep.CoursesRep.GetCourse(id);


            return View(course);
        }

        public async Task<IActionResult> GetEvents()
        {
            List<EventsNews> EventNews = appRep.EventRep.GetOnlyEvents();
            List<DisplayEvents> DisplayEvent = new();
            foreach(var eventItem in EventNews)
            {
                DisplayEvent.Add(new DisplayEvents()
                {
                    Date= eventItem.Date,
                    Description = eventItem.DetailsEn,
                    Title = eventItem.TitleEn,
                    Id = eventItem.Id
                });
            }

            return Json(DisplayEvent);
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