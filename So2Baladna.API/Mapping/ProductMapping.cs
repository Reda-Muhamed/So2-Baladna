using AutoMapper;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Product;

namespace So2Baladna.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.Categoryname, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Product, ProductAddDto>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ReverseMap();
        }
    }
    
}
