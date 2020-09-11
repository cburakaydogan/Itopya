using Itopya.Domain.Entities.Concrete;
using Itopya.Domain.Repositories;
using Itopya.Infrastructure.Context;

namespace Itopya.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
