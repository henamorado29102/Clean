using AutoMapper;
using Clean.Core.Entities;
using Clean.WebApi.DTOs;

namespace Clean.WebApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
