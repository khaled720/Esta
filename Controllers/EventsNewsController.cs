using AutoMapper;
using ESTA.Helpers;
using ESTA.Mappers;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;

namespace ESTA.Controllers
{
    public class EventsNewsController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IMapper mapper;
        private readonly string culture;

        public EventsNewsController(IUnitOfWork appRep, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            this.appRep = appRep;
            this.mapper = mapper;

            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    



        public IActionResult Index()
        {
            var flagNews = (int)Flag.News;
            var flagEvents = (int)Flag.Events;
            var Social = (int)EventType.Social;
            var Esta = (int)EventType.Esta;

            List<DisplayEvents> News = EventsModelToEventsDisplay(0, flagNews, null);
            List<DisplayEvents> EstaEvent = EventsModelToEventsDisplay(0, flagEvents, Esta);
            List<DisplayEvents> SocialEvent = EventsModelToEventsDisplay(0, flagEvents, Social);

            List<DisplayEventPartial> eventPartial = new()
            {
                new DisplayEventPartial()
                {
                    EventsList = News,
                    DivId = "News",
                    Flag = flagNews,
                    EventType = null,
                    ViewMore = appRep.EventRep.CheckEvents(1, flagNews, null)
                },
                new DisplayEventPartial()
                {
                    EventsList = EstaEvent,
                    DivId = "Esta",
                    Flag = flagEvents,
                    EventType = Esta,
                    ViewMore = appRep.EventRep.CheckEvents(1, flagEvents, Esta)
                },
                new DisplayEventPartial()
                {
                    EventsList = SocialEvent,
                    DivId = "Social",
                    Flag = flagEvents,
                    EventType = Social,
                    ViewMore = appRep.EventRep.CheckEvents(1, flagEvents, Social)
                }
            };
            return View(eventPartial);
        }
        public IActionResult CheckMoreEvents(int page, int Flag, int EventType)
        {
            bool CheckEvent = appRep.EventRep.CheckEvents(page + 1, Flag, EventType);

            return Json(CheckEvent);
        }
        public IActionResult GetEventsPage(int page, int Flag, int? EventType)
        {
            List<DisplayEvents> EventsNews = EventsModelToEventsDisplay(page, Flag, EventType);

            return PartialView("_renderEvent", EventsNews);
        }
        public IActionResult GetEvent(int id)
        {
            EventsNews events = appRep.EventRep.GetEventById(id);
            if (events != null)
            {
                DisplayEvents displayEvents = new()
                {
                    Id = events.Id,
                    Image = events.Image,
                    Date = events.Date,
                    Title = culture == "en" ? events.TitleEn : events.TitleAr,
                    Description = culture == "en" ? events.DetailsEn : events.DetailsAr
                };

                return View(displayEvents);
            }
            else
                return RedirectToAction("Error"); ;
        }

        private List<DisplayEvents> EventsModelToEventsDisplay(int page, int Flag, int? Eventtype)
        {
            List<EventsNews> EventsList = appRep.EventRep.GetEvents(page, Flag, Eventtype);

            List<DisplayEvents> EventsNews = new();
            string details;
            string title;
            foreach (var eventVar in EventsList)
            {
                if (culture == "en")
                {
                    details = HtmlHelper.RemoveHTMLTags(eventVar.DetailsEn);
                    title = eventVar.TitleEn;
                }
                else
                {
                    details = HtmlHelper.RemoveHTMLTags(eventVar.DetailsAr);
                    title = eventVar.TitleAr;
                }
                if (title.Length > 25)
                    title = title.Substring(0, 25) + "...";
                EventsNews.Add(new()
                {
                    Id = eventVar.Id,
                    Image = eventVar.Image,
                    Title = title,
                    Date = eventVar.Date,
                    Flag = eventVar.Flag,
                    Description = details,
                });
            }
            return EventsNews;
        }



    }
}
