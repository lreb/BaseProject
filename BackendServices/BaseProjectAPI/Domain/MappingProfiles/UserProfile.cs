using AutoMapper;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;

namespace BaseProjectAPI.Domain.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserAuthenticateViewModel>();
        }
    }
}
