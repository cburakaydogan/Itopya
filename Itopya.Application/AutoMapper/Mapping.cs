using AutoMapper;
using Itopya.Application.Models.Category;
using Itopya.Application.Models.Product;
using Itopya.Domain.Entities.Concrete;
using Itopya.Application.Models.CategoryBundle;

namespace Itopya.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<ProductDto, ProductCreateDto>().ReverseMap();

            CreateMap<CategoryBundle, CategoryBundleDto>().ReverseMap();

            CreateMap<CategoryBundle, CategoryBundleAddDto>().ReverseMap();
        }
    }
}
