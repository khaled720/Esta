using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;

namespace ESTA.Repository
{
    public class EventRepo : IEventsRepo
    {
        private readonly AppDbContext context;

        public EventRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void AddEvent(EventsNews Event)
        {
            context.Add(Event);
        }

        public bool CheckEvents(int page, int Flag, int? EventType, int count = 3)
        {
            if (Flag == 1)
            {
                return context.EventsNews
                .Where(x => x.Flag == Flag)
                .Skip(page * count)
                .Take(count).Any();
            }
            return context.EventsNews
                .Where(x => x.Flag == Flag && x.EventType == EventType)
                .Skip(page * count)
                .Take(count).Any();
        }

        public void DeleteEvent(EventsNews events)
        {
            context.EventsNews.Remove(events);
        }
        public EventsNews FindEvent(int Id)
        {
            return context.EventsNews.Find(Id);
        }

        public EventsNews GetEventById(int Id)
        {
            return context.EventsNews.Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<EventsNews> GetEvents(int page, int Flag, int? EventType, int count = 3)
        {
            if (Flag == 1)
            {
                return context.EventsNews
               .Where(x => x.Flag == Flag)
               .OrderBy(x => x.Date)
               .Skip(page * count)
               .Take(count)
               .ToList();
            }
            return context.EventsNews
                .Where(x => x.Flag == Flag && x.EventType == EventType)
                .OrderBy(x => x.Date)
                .Skip(page * count)
                .Take(count)
                .ToList();
        }

        public EventsNews GetLatestEvent()
        {
            return context.EventsNews.Where(x => x.Date >= DateTime.Now.Date && x.Flag == 0)
                .OrderBy(x => x.Date)
                .FirstOrDefault();
        }

        public List<EventsNews> GetOnlyEvents()
        {
            return context.EventsNews.Where(x => x.Flag == 0).ToList();
        }
        public EventsNews GetLatestEvent(int Flag, int? EventType)
        {
            EventsNews? eventsNews = new() ;
            if (Flag == 1)
            {
                eventsNews = context.EventsNews.Where(x => x.Date >= DateTime.Now.Date && x.Flag == Flag)
                    .OrderBy(x => x.Date)
                    .FirstOrDefault();

                eventsNews ??= context.EventsNews.Where(x => x.Flag == Flag)
                    .OrderBy(x => x.Date)
                    .FirstOrDefault();
            }

            else
            {
                eventsNews = context.EventsNews.Where(x => x.Date >= DateTime.Now.Date && x.Flag == Flag && x.EventType == EventType)
                    .OrderBy(x => x.Date)
                    .FirstOrDefault();

                eventsNews ??= context.EventsNews.Where(x => x.Flag == Flag && x.EventType == EventType)
                    .OrderBy(x => x.Date)
                    .FirstOrDefault();
            }


            return eventsNews;

        }
    }
}
