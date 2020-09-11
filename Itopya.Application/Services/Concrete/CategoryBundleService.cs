using AutoMapper;
using Itopya.Application.Models.CategoryBundle;
using Itopya.Application.Services.Abstract;
using Itopya.Application.Utilities;
using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itopya.Application.Services.Concrete
{
    public class CategoryBundleService : ICategoryBundleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryBundleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryBundleDto> AddCategory(CategoryBundleAddDto model)
        {
            var modelToBundle = _mapper.Map<CategoryBundle>(model);

            var category = await _unitOfWork.CategoryBundle.GetById(modelToBundle.CategoryId);

            if (category != null)
                throw new HttpException(400, "Category Exists");

            await _unitOfWork.CategoryBundle.Add(category);
            await _unitOfWork.Commit();

            var mapped = _mapper.Map<CategoryBundleDto>(category); // CreatedAtRoute için
            return mapped;
        }
        public async Task<CategoryBundleDto> GetCategory(int id)
        {
            var category = await _unitOfWork.CategoryBundle.GetById(id);

            if (category == null)
                throw new HttpException(404, "Category Not Found");

            var mapped = _mapper.Map<CategoryBundleDto>(category);

            return mapped;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryBundle = await _unitOfWork.CategoryBundle.FirstOrDefault(x => x.CategoryId == id);
            if (categoryBundle == null)
                return false;

            _unitOfWork.CategoryBundle.Delete(categoryBundle);
            return await _unitOfWork.Commit();
        }
        public async Task<List<CategoryBundleDto>> GetCategoryBundles()
        {
            var categories = await _unitOfWork.CategoryBundle.GetList();

            var mapped = _mapper.Map<List<CategoryBundleDto>>(categories);
            return mapped;
        }
    }
}
