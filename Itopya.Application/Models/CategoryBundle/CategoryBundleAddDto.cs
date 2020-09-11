using System.ComponentModel.DataAnnotations;

namespace Itopya.Application.Models.CategoryBundle
{
    public class CategoryBundleAddDto
    {
        [Required(ErrorMessage = "CategoryId required." )]
        public int CategoryId { get; set; }
        public bool IsRequired { get; set; }
    }
}
