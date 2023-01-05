using System.Diagnostics;
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

        public HomeController(
            ILogger<HomeController> logger,
            IUnitOfWork appRep,
            IWebHostEnvironment hostEnvironment,
            IHttpContextAccessor contextAccessor
        )
        {
            _logger = logger;
            this.Uow = appRep;
            this.hostEnvironment = hostEnvironment;

            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
        }

        public async Task<IActionResult> Contact()
        {
            var contact = await Uow.ContactRep.GetAllContacts();

            return View(contact);
        }

     
        public async Task<IActionResult> Index()
        {
            List<Course> courses = (List<Course>)await Uow.CoursesRep.GetAllCourses();
            return View(courses);
        }

        public async Task<IActionResult> About(string type)
        {
            var content = await Uow.ContentRep.GetContent(type);

            switch (type)
            {
                case "ta":
                    ViewBag.about = "Technical Analysis";
                    break;

                case "ethics":
                    ViewBag.about = "Code Of Ethics \"Bylaws\"";
                    break;
                case "ifta":
                    ViewBag.about = "IFTA";
                    break;
                case "benefits":
                    ViewBag.about = "Benefits Of Membership";
                    break;
                default:
                    ViewBag.about = "About";
                    break;
            }
            return View(content);
        }

        public async Task<IActionResult> GetEvents()
        {
            List<EventsNews> EventNews = Uow.EventRep.GetOnlyEvents();
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
              
            }
         
            return Json(EventNews);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
