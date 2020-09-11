using System.ComponentModel.DataAnnotations;

namespace Itopya.Application.Models.Category
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }

        [Required(ErrorMessage = "Can't be null"), StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters."),
            Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
