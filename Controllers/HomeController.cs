using System.Diagnostics;
using System.Text.RegularExpressions;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ESTA.Controllers
{
   

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork Uow;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IStringLocalizer localizer;
        private readonly string culture;

        public HomeController(
            ILogger<HomeController> logger,
            IUnitOfWork appRep,
            IWebHostEnvironment hostEnvironment,
            IHttpContextAccessor contextAccessor,
            IStringLocalizer localizer
        )
        {
            _logger = logger;
            this.Uow = appRep;
            this.hostEnvironment = hostEnvironment;
            this.localizer = localizer;
            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
        }

        public async Task<IActionResult> Contact()
        {
            var contact = await Uow.ContactRep.GetAllContacts();

            return View(contact);
        }

     

        public  IActionResult Index()
        {
            ViewBag.welcome = localizer["hi"];
            var hivm = new HomeIndexViewModel();
            try
            {
            hivm.About = Uow.ContentRep.GetContent("about").GetAwaiter().GetResult().DescriptionEn;
            hivm.Mission = Uow.ContentRep.GetContent("mission").GetAwaiter().GetResult().DescriptionEn;
            hivm.Vission = Uow.ContentRep.GetContent("vission").GetAwaiter().GetResult().DescriptionEn;

            }
            catch (Exception)
            {

            }
        

            return View(hivm);
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
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
