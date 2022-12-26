using AutoMapper;
using ESTA.Models;
using ESTA.ViewModels;

namespace ESTA.Mappers
{
    public class EventsMapper : Profile
    {
        public EventsMapper()
        {
            CreateMap<EventsNews, CreateEvent>();
            CreateMap<CreateEvent, EventsNews>();
            CreateMap<EventsNews, EditEvents>();
        }
    }
}
