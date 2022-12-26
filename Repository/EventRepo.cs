using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;

namespace ESTA.Repository
{
    public class EventRepo:IEventsRepo
    {
        private readonly AppDbContext context;

        public EventRepo(AppDbContext context) {
            this.context = context;
        }

        public void AddEvent(EventsNews Event)
        {
            context.Add(Event);
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
            return context.EventsNews.Where(x=>x.Id == Id).FirstOrDefault();
        }

        public List<EventsNews> GetEvents()
        {
            return context.EventsNews.ToList();
        }

        public List<EventsNews> GetOnlyEvents()
        {
            return context.EventsNews.Where(x => x.Flag == 0).ToList();
        }
    }
}
