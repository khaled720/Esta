using ESTA.Models;
using ESTA.ViewModels;

namespace ESTA.Repository.IRepository
{
    public interface IEventsRepo
    {
        public void AddEvent(EventsNews Event);
        public List<EventsNews> GetEvents();
        public EventsNews GetEventById(int Id);
        public void DeleteEvent(EventsNews events);
        public EventsNews FindEvent(int Id);
    }
}
