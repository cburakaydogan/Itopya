using Itopya.Application.Models.Category;
using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itopya.Application.Services.Abstract
{
    public interface ICategoryService
    {
        Task<PagedList<Category>> GetAllCategories(CategoryParameters parameters);
        Task<CategoryDto> GetCategory(int id);
        Task<CategoryDto> CreateCategory(CategoryCreateDto model);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(CategoryUpdateDto model);
        Task<List<int>> RecursiveParentToChild(int id);
        Task<List<Category>> GetList();
    }
}
