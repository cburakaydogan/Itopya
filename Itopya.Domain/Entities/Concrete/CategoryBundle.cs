using Itopya.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Itopya.Domain.Entities.Concrete
{
    public class CategoryBundle : IBaseEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsRequired { get; set; }
    }
}
