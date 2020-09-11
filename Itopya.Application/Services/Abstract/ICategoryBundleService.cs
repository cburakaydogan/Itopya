using Itopya.Application.Models.CategoryBundle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itopya.Application.Services.Abstract
{
    public interface ICategoryBundleService
    {
        Task<CategoryBundleDto> AddCategory(CategoryBundleAddDto model);
        Task<List<CategoryBundleDto>> GetCategoryBundles();
        Task<bool> DeleteCategory(int id);
        Task<CategoryBundleDto> GetCategory(int id);
    }
}
