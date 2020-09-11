using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Itopya.Application.Models.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Can't be null"), StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters."),
            Display(Name = "Category Name")
        ]
        public string Name { get; set; }

        private int? parentCategoryId;
        public int? ParentCategoryId
        {
            get
            {
                if (parentCategoryId == 0)
                    return null;
                else
                    return parentCategoryId;
            }
            set { parentCategoryId = value; }
        }
    }
}