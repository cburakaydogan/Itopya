namespace Itopya.Application.Models.Category
{
    public class CategoryDto
    {

        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
       
    }
}