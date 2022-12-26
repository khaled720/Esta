using System.Diagnostics;
using AutoMapper;
using ESTA.Migrations;
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
        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly string culture;

        public HomeController(ILogger<HomeController> logger, IAppRep appRep, IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;

            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
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
            string title;
            foreach (var eventItem in EventNews)
            {
                if (culture == "en")
                {
                    title = eventItem.TitleEn;
                }
                else
                {
                    title = eventItem.TitleAr;
                }
                DisplayEvent.Add(new DisplayEvents()
                {
                    Date= eventItem.Date,
                    Title = title,
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