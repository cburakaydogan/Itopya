using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.Repositories;
using Itopya.Infrastructure.Context;

namespace Itopya.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
