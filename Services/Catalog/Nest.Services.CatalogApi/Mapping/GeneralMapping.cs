using AutoMapper;
using Nest.Services.CatalogApi.DTO_s.Category;
using Nest.Services.CatalogApi.DTO_s.Product;
using Nest.Services.CatalogApi.DTO_s.SubCategory;
using Nest.Services.CatalogApi.Models;

namespace Nest.Services.CatalogApi.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<SubCategory, SubCategoryDto>().ReverseMap();

        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
    }
}
