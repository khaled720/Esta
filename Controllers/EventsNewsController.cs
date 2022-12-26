using AutoMapper;
using ESTA.Helpers;
using ESTA.Mappers;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace ESTA.Controllers
{
    public class EventsNewsController : Controller
    {
        private readonly IAppRep appRep;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHost;

        public EventsNewsController(IWebHostEnvironment webHost, IAppRep appRep, IMapper mapper)
        {
            this.appRep = appRep;
            this.mapper = mapper;
            this.webHost = webHost;
        }
        public IActionResult Index()
        {
            List<EventsNews> EventsList = appRep.EventRep.GetEvents();
            DisplayEventObj ListObj = new();
            List<DisplayEvents> Events = new();
            List<DisplayEvents> News = new();
            foreach (var eventVar in EventsList)
            {
                if (eventVar.Flag == 0)
                {
                    Events.Add(new()
                    {
                        Id = eventVar.Id,
                        Image = eventVar.Image,
                        Title = eventVar.TitleAr,
                        Flag = eventVar.Flag,
                        Description = RemoveHTMLTags(eventVar.DetailsAr).Trim().Substring(1, 20),
                    });
                }
                else
                {
                    News.Add(new()
                    {
                        Id = eventVar.Id,
                        Image = eventVar.Image,
                        Title = eventVar.TitleAr,
                        Flag = eventVar.Flag,
                        Description = RemoveHTMLTags(eventVar.DetailsAr).Substring(1, 20),
                    });
                }

            }
            ListObj.Events = Events;
            ListObj.News = News;

            return View(ListObj);
        }
        public IActionResult GetEvent(int id)
        {
            EventsNews events = appRep.EventRep.GetEventById(id);
            DisplayEvents displayEvents = new()
            {
                Id = events.Id,
                Description = events.DetailsAr,
                Title = events.TitleAr,
                Image = events.Image
            };

            return View(displayEvents);
        }
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(CreateEvent Event)
        {
            var DescArDecoded = HttpUtility.HtmlDecode(Event.DetailsAr);
            var DescEnDecoded = HttpUtility.HtmlDecode(Event.DetailsEn);
            var imgName = ImageHelper.UploadedFile(Event.Image, "images/News/");
            EventsNews NewEvent = mapper.Map<CreateEvent, EventsNews>(Event);

            NewEvent.DetailsAr = DescArDecoded;
            NewEvent.DetailsEn = DescEnDecoded;
            NewEvent.Image = "/images/News/" + imgName;
            appRep.EventRep.AddEvent(NewEvent);
            await appRep.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult EditEvent(int id)
        {
            EventsNews Event = appRep.EventRep.GetEventById(id);
            EditEvents editEvent = mapper.Map<EventsNews, EditEvents>(Event);

            return View(editEvent);
        }
        [HttpPost]
        public async Task<IActionResult> EditEventAsync(EditEvents EditEvent)
        {
            EventsNews Event = appRep.EventRep.GetEventById(EditEvent.Id);
            var DescArDecoded = HttpUtility.HtmlDecode(EditEvent.DetailsAr);
            var DescEnDecoded = HttpUtility.HtmlDecode(EditEvent.DetailsEn);
            if(EditEvent.ImageUpload != null)
            {
                var imgName = ImageHelper.UploadedFile(EditEvent.ImageUpload, "/images/News/");
                Event.Image = "/images/News/" + imgName;
            }
            Event.DetailsAr = DescArDecoded;
            Event.DetailsEn = DescEnDecoded;
            Event.TitleAr = EditEvent.TitleAr;
            Event.TitleEn = EditEvent.TitleEn;
            if(EditEvent.Date != null)
            {
                Event.Date = Event.Date;
            }
            await appRep.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            EventsNews events = appRep.EventRep.FindEvent(id);
            if (events != null)
            {
                appRep.EventRep.DeleteEvent(events);
                await appRep.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        private string RemoveHTMLTags(string text)
        {
            return Regex.Replace(text.Trim(), "<.*?>", String.Empty);
        }
    }
}
