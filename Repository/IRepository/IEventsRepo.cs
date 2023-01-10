using ESTA.Models;
using ESTA.ViewModels;

namespace ESTA.Repository.IRepository
{
    public interface IEventsRepo
    {
        public void AddEvent(EventsNews Event);
        public List<EventsNews> GetEvents(int page, int count = 3);
        public List<EventsNews> GetOnlyEvents();
        public EventsNews GetEventById(int Id);
        public void DeleteEvent(EventsNews events);
        public EventsNews FindEvent(int Id);
        public EventsNews GetLatestEvent();
        public bool CheckEvents(int page, int count = 3);
    }
}
