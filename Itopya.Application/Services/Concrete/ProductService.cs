using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Itopya.Application.Models.Product;
using Itopya.Application.Services.Abstract;
using Itopya.Application.Utilities.ImageUpload;
using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.Entities.RequestFeatures;
using Itopya.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Itopya.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public readonly IFileUpload _upload;
        private readonly ICategoryService _categoryService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IFileUpload upload, ICategoryService categoryService)
        {
            _upload = upload;
            _categoryService = categoryService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<ProductDto>> GetProducts(ProductParameters parameters)
        {
            var categoryIds = await _categoryService.RecursiveParentToChild(parameters.CategoryId);

            var products = await _unitOfWork.Product.GetFilteredList(parameters,
                 predicate: x => x.Where(y => categoryIds.Contains(y.CategoryId)),
                 include: z => z.Include(x => x.Category));

            var mapped = _mapper.Map<PagedList<Product>, PagedList<ProductDto>>(products);
           
            return mapped;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _unitOfWork.Product.GetById(id);

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> CreateProduct(ProductCreateDto model)
        {
            var product = _mapper.Map<Product>(model);

            product.ImagePath = await _upload.SaveFile(model.Image);

            await _unitOfWork.Product.Add(product);

            await _unitOfWork.Commit();

            var mapped = _mapper.Map<ProductDto>(product); // CreatedAtRoute için
            return mapped;
        }

        public async Task<bool> UpdateProduct(ProductUpdateDto model)
        {
            var product = await _unitOfWork.Product.GetById(model.Id);

            if (product == null)
                return false;

            if (model.Image != null)
                product.ImagePath = await _upload.UpdateFile(model.Image, product.ImagePath);

            var mapped = _mapper.Map(model,product);

            _unitOfWork.Product.Update(mapped);
            return await _unitOfWork.Commit();
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return false;

            _upload.RemoveFile(product.ImagePath);

            _unitOfWork.Product.Delete(product);
            return await _unitOfWork.Commit();
        }
    }
}