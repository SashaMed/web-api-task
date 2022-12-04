using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;


namespace WebAPI.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Fridge, FridgeDto>();
            CreateMap<Product, ProductDto>();

            CreateMap<FridgeCreationDto, Fridge>();
            CreateMap<ProductCreationDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
