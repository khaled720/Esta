using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ESTA.ViewComponents
{
    public class UpcomingEventsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork Uow;
        private string culture;
        public UpcomingEventsViewComponent(IUnitOfWork appRep, IHttpContextAccessor contextAccessor)
        {
            Uow = appRep;
            var rqf = contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            culture = rqf.RequestCulture.Culture.Name;
        }
        public IViewComponentResult Invoke()
        {
            EventsNews eventItem = Uow.EventRep.GetLatestEvent();
            DisplayEvents? LatestEvent;
            if (eventItem != null)
            {
                LatestEvent = new();
                string details = culture == "en" ? eventItem.DetailsEn : eventItem.DetailsAr;
                LatestEvent.Date = eventItem.Date;
                LatestEvent.Title = culture == "en" ? eventItem.TitleEn : eventItem.TitleAr;
                LatestEvent.Description = RemoveHTMLTags(details).Substring(1, 20);
                LatestEvent.Id = eventItem.Id;
                LatestEvent.Image = eventItem.Image;
                LatestEvent.Flag = eventItem.Flag;
            }
            else
                LatestEvent = null;

            return View("_upcomingEvent", LatestEvent);
        }
        private static string RemoveHTMLTags(string text)
        {
            return Regex.Replace(text.Trim(), "<.*?>", String.Empty);
        }
    }
}
