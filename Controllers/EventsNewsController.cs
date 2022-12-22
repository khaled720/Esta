using AutoMapper;
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
        [HttpPost]
        public IActionResult SaveImg(IFormFile Image)
        {
            string ImgName = UploadedFile(Image);
            var ImgPath = "/images/News/" + ImgName;

            return Json(ImgPath);
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
            var imgName = UploadedFile(Event.Image);
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
        public async Task<IActionResult> EditEventAsync(EditEvents events)
        {
            EventsNews Event = appRep.EventRep.GetEventById(events.Id);
            mapper.Map(events, Event);
            await appRep.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost]
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
        [NonAction]
        private string UploadedFile(IFormFile Image)
        {
            string uniqueFileName;

            string uploadsFolder = Path.Combine(webHost.WebRootPath, "images\\News");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        private string RemoveHTMLTags(string text)
        {
            return Regex.Replace(text.Trim(), "<.*?>", String.Empty);
        }
    }
}
