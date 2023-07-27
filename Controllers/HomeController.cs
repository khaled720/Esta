using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.Resources;
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

           private readonly IStringLocalizer<ESTA.SharedResource> localizer;
        private readonly string culture;

        public HomeController(
            ILogger<HomeController> logger,
            IUnitOfWork appRep,
            IWebHostEnvironment hostEnvironment,
            IHttpContextAccessor contextAccessor,
                IStringLocalizer<ESTA.SharedResource> localizer
        )
        {
            _logger = logger;
            this.Uow = appRep;
            this.hostEnvironment = hostEnvironment;
               this.localizer = localizer;
         //   var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
               culture = Thread.CurrentThread.CurrentCulture.Name;
        }

        public async Task<IActionResult> Contact()
        {
            var contact = await Uow.ContactRep.GetAllContacts();

            return View(contact);
        }

        public async Task<IActionResult> Index()
        {
         


               var hivm = new HomeIndexViewModel();
            try
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "ar") {



                    hivm.About = Regex.Replace(
                        Uow.ContentRep.GetContent("about").DescriptionAr ?? "",
                        "<.*?>",
                        String.Empty
                    );
                    hivm.Mission = Regex.Replace(
                        Uow.ContentRep.GetContent("mission").DescriptionAr ?? "",
                        "<.*?>",
                        String.Empty
                    );
                    hivm.Vission = Regex.Replace(
                        Uow.ContentRep.GetContent("vission").DescriptionAr ?? "",
                        "<.*?>",
                        String.Empty
                    );


                }

                else
                {



                hivm.About = Regex.Replace(
                    Uow.ContentRep.GetContent("about").DescriptionEn??"",
                    "<.*?>",
                    String.Empty
                );
                hivm.Mission = Regex.Replace(
                    Uow.ContentRep.GetContent("mission").DescriptionEn??"",
                    "<.*?>",
                    String.Empty
                );
                hivm.Vission = Regex.Replace(
                    Uow.ContentRep.GetContent("vission").DescriptionEn??"",
                    "<.*?>",
                    String.Empty
                );


                }







                if ( hivm.About.Length > 400) 
                {

                    hivm.About = hivm.About.Substring(0,400);
               
                }
                if (hivm.Vission.Length > 400)
                {

                    hivm.Vission = hivm.Vission.Substring(0, 400);

                }
                if (hivm.Mission.Length > 400)
                {

                    hivm.Mission = hivm.Mission.Substring(0, 400);

                }





                hivm.UpcomingCourse = await this.Uow.CoursesRep.GetUpcomingCourse();




            }
            catch (Exception ex)
            {
                hivm.About = String.Empty;
                hivm.Mission = String.Empty;
                hivm.Vission = String.Empty;
            }

            return View(hivm);
        }

        public async Task<IActionResult> About(string type)
        {
            var content = Uow.ContentRep.GetContent(type);

            switch (type)
            {
                case "ta":
                    ViewBag.about = localizer["ta"];
                    break;
                case "mission":
                    ViewBag.about = localizer["ourmission"];
                    break;

                case "vission":
                    ViewBag.about = localizer["ourvission"];
                    break;

                case "ethics":
                    ViewBag.about = localizer["ethics"];
                    break;
                case "ifta":
                    ViewBag.about = localizer["ifta"];
                    break;
                case "benefits":
                    ViewBag.about = localizer["benefits"];
                    break;
                default:
                    ViewBag.about = localizer["about"];
                    break;
            }

            return View(content);
        }

        public IActionResult GetEvents()
        {
            List<ESTA.Models.EventsNews> EventNews = Uow.EventRep.GetOnlyEvents();
            List<DisplayEvents> DisplayEvent = new();

            foreach (var eventItem in EventNews)
            {
                DisplayEvent.Add(
                    new DisplayEvents()
                    {
                        Date = eventItem.Date,
                        Title = culture == "en" ? eventItem.TitleEn : eventItem.TitleAr,
                        Id = eventItem.Id,
                        EventType = eventItem.EventType
                    }
                );
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
