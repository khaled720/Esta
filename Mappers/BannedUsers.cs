using AutoMapper;
using ESTA.Models;
using ESTA.ViewModels;

namespace ESTA.Mappers
{
    public class BannedUsers : Profile
    {
        public BannedUsers()
        {
            CreateMap<ForumBannedUser, ViewBanned>()
                .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ModName,
                opt => opt.MapFrom(src => src.Mod.UserName))
                .ForMember(dest => dest.ForumTitle,
                opt => opt.MapFrom(src => src.Forum.Title))
                .ForMember(dest => dest.Date,
                opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd hh:mm:ss tt")));
        }
    }
}
