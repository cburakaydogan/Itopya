using AutoMapper;
using Itopya.Application.Models.Category;
using Itopya.Application.Models.Product;
using Itopya.Domain.Entities.Concrete;
using Itopya.Application.Models.CategoryBundle;
using Itopya.Domain.Entities.RequestFeatures;
using System.Collections.Generic;

namespace Itopya.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<PagedList<Category>, PagedList<CategoryDto>>().ConvertUsing<PageListConverter<Category, CategoryDto>>();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<ProductDto, ProductCreateDto>().ReverseMap();
            CreateMap<PagedList<Product>, PagedList<ProductDto>>().ConvertUsing<PageListConverter<Product, ProductDto>>();

            CreateMap<CategoryBundle, CategoryBundleDto>().ReverseMap();
            CreateMap<CategoryBundle, CategoryBundleAddDto>().ReverseMap();
        }
    }
}
