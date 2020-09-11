using Itopya.Application.Models.Product;
using Itopya.Domain.Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Itopya.Application.Services.Abstract
{
    public interface IProductService
    {
        Task<PagedList<ProductDto>> GetProducts(ProductParameters parameters);
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> CreateProduct(ProductCreateDto model);
        Task<bool> UpdateProduct(ProductUpdateDto model);
        Task<bool> DeleteProduct(int id);
       
    }
}
