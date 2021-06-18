using AutoMapper;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Domain.ViewModels;
using BaseProjectAPI.Service.Items.Commands;

namespace BaseProjectAPI.Domain.MappingProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemViewModel>();

            CreateMap<CreateItemCommand, Item>();

            CreateMap<UpdateItemCommand, Item>();
        }
    }
}
