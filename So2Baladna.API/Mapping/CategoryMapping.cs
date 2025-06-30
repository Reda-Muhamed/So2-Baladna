using AutoMapper;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities.Product;

namespace So2Baladna.API.Mapping
{
    public class CategoryMapping: Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }
    }
}
