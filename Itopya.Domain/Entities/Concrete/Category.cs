using Itopya.Domain.Entities.Abstract;
using System.Collections.Generic;

namespace Itopya.Domain.Entities.Concrete
{
    public class Category : IBaseEntity
    {
        public int Id { get; set; }

        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }

        public Category ParentCategory { get; set; }

        public CategoryBundle CategoryBundle { get; set; }

        public ICollection<Category> SubCategories { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
