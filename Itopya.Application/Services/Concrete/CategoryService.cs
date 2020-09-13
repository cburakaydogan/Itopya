using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Itopya.Application.Models.Category;
using Itopya.Application.Services.Abstract;
using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.Entities.RequestFeatures;
using Itopya.Domain.UnitOfWork;

namespace Itopya.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> CreateCategory(CategoryCreateDto model)
        {
            var category = _mapper.Map<Category>(model);

            await _unitOfWork.Category.Add(category);
            await _unitOfWork.Commit();

            var mapped = _mapper.Map<CategoryDto>(category); // CreatedAtRoute için
            return mapped;
        }

        public async Task<bool> UpdateCategory(CategoryUpdateDto model)
        {
            var isExistCategory = _unitOfWork.Category.FirstOrDefault(x => x.Id == model.Id);
            if(isExistCategory == null)
            {
                return false;
            }
            var category = _mapper.Map<Category>(model);
            _unitOfWork.Category.Update(category);

            return await _unitOfWork.Commit();
        }
       
        public async Task<PagedList<CategoryDto>> GetAllCategories(CategoryParameters parameters)
        {
            var categories = await _unitOfWork.Category.GetFilteredList(parameters,
                selector : y => new Category
                {
                    Id = y.Id,
                    Name = y.Name,
                    ParentCategoryId = y.ParentCategoryId,
                    SubCategories = y.SubCategories.Where(z => z.ParentCategoryId == y.Id).ToList()
                });

            var mapped = _mapper.Map<PagedList<Category>, PagedList<CategoryDto>>(categories);

            return mapped;
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            var category = await _unitOfWork.Category.FirstOrDefault(predicate: x => x.Id == id,
                selector: y => new Category
                {
                    Id = y.Id,
                    Name = y.Name,
                    ParentCategoryId = y.ParentCategoryId
                });

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var result = await _unitOfWork.Category.ExecuteSqlRaw("spCategoryDelete {0}", id);

            if (result> 0)
                return true;
            return false;
        }

        public async Task<List<Category>> GetList()
        {
            var categories = await _unitOfWork.Category.GetList();
            return categories;
        }

        public async Task<List<int>> RecursiveParentToChild(int id)
        {
            var relatedCategories = await _unitOfWork.Category.FromSqlRaw("EXEC spCategoryRecursiveParentToChild {0}", id);

            List<int> relatedCategoriesId = relatedCategories.Select(x => x.Id).ToList();

            return relatedCategoriesId;
        }

       

    }
}