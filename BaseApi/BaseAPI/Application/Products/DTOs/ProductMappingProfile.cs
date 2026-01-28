using AutoMapper;
using BaseAPI.Domain.Entities;

namespace BaseAPI.Application.Products.DTOs;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
    }
}
