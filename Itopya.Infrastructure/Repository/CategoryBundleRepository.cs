using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.Repositories;
using Itopya.Infrastructure.Context;

namespace Itopya.Infrastructure.Repository
{
    public class CategoryBundleRepository : BaseRepository<CategoryBundle>, ICategoryBundleRepository
    {
        public CategoryBundleRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
