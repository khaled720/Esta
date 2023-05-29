using AutoMapper;
using ESTA.Models;
using ESTA.ViewModels;

namespace ESTA.Mappers
{
    public class UserMapper :Profile
    {

        public UserMapper()
        {
            CreateMap<User,EditUserPersonalInfoViewModel>();
            CreateMap<EditUserPersonalInfoViewModel,User>();


            CreateMap<User, EditUserAddressInfoViewModel>();
            CreateMap<EditUserAddressInfoViewModel, User>();


            CreateMap<User, EditUserEducationInfoViewModel>();
            CreateMap<EditUserEducationInfoViewModel, User>();



            CreateMap<User, EditUserWorkInfoViewModel>();
            CreateMap<EditUserWorkInfoViewModel, User>();

        }


    }
}
