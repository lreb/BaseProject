using AutoMapper;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;

namespace BaseProjectAPI.Domain.MappingProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemViewModel>().ForMember(destination => destination.HealthyStock, opt => opt.Condition(src => !(src.Quantity > 2 )));
        }
    }
}
