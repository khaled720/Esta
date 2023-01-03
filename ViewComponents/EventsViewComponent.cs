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
            List<EventsNews> EventNews = Uow.EventRep.GetEvents(0);
            List<DisplayEvents> DisplayEvent = new();
            string details;
            foreach (var eventItem in EventNews)
            {
                details = culture == "en" ? eventItem.DetailsEn : eventItem.DetailsAr;
                DisplayEvent.Add(new DisplayEvents()
                {
                    Date = eventItem.Date,
                    Title = culture == "en" ? eventItem.TitleEn : eventItem.TitleAr,
                    Description = RemoveHTMLTags(details).Substring(1, 20),
                    Id = eventItem.Id,
                    Image = eventItem.Image,
                    Flag = eventItem.Flag
                });
            }
            return View("_renderEvents", DisplayEvent);
        }
        private static string RemoveHTMLTags(string text)
        {
            return Regex.Replace(text.Trim(), "<.*?>", String.Empty);
        }
    }
}
