using System.Diagnostics;
using System.Text.RegularExpressions;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork Uow;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly string culture;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork appRep, IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            this.Uow = appRep;
            this.hostEnvironment = hostEnvironment;

            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
        }

        public async Task<IActionResult> Index()
        {


            List<Course> courses = (List<Course>)await Uow.CoursesRep.GetAllCourses();
            return View(courses);
        }




        public async Task<IActionResult> CourseDetails(int id)
        {
            var course = await Uow.CoursesRep.GetCourse(id);


            return View(course);
        }

        public IActionResult GetEvents()
        {
            List<EventsNews> EventNews = Uow.EventRep.GetOnlyEvents();
            List<DisplayEvents> DisplayEvent = new();

            foreach (var eventItem in EventNews)
            {
                DisplayEvent.Add(new DisplayEvents()
                {
                    Date = eventItem.Date,
                    Title = culture == "en"? eventItem.TitleEn: eventItem.TitleAr,
                    Id = eventItem.Id,
                    EventType = eventItem.EventType
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

