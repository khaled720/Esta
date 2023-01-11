using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ESTA.ViewComponents
{
    public class EventsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork Uow;
        private string culture;
        public EventsViewComponent(IUnitOfWork appRep, IHttpContextAccessor contextAccessor)
        {
            Uow = appRep;
            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
        }

        public IViewComponentResult Invoke()
        {
            var flagNews = (int)Flag.News;
            var flagEvents = (int)Flag.Events;
            var Social = (int)EventType.Social;
            var Esta = (int)EventType.Esta;

            List<DisplayEvents> DisplayEvent = new();
            List<EventsNews> EventNews = new()
            {
                Uow.EventRep.GetLatestEvent(flagNews,null),
                Uow.EventRep.GetLatestEvent(flagEvents,Social),
                Uow.EventRep.GetLatestEvent(flagEvents,Esta)
            };
            string details;
            foreach (var eventItem in EventNews)
            {
                if (eventItem != null)
                {
                    details = culture == "en" ? eventItem.DetailsEn : eventItem.DetailsAr;
                    DisplayEvent.Add(new DisplayEvents()
                    {
                        Date = eventItem.Date,
                        Title = culture == "en" ? eventItem.TitleEn : eventItem.TitleAr,
                        Description = HtmlHelper.RemoveHTMLTags(details),
                        Id = eventItem.Id,
                        Image = eventItem.Image,
                        Flag = eventItem.Flag
                    });
                }
            }
            return View("_renderEvents", DisplayEvent);
        }
    }
}
