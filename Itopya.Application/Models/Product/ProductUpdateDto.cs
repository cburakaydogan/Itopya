using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itopya.Application.Models.Product
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Can't be null")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Can't be null"), StringLength(150, ErrorMessage = "Name cannot be longer than 150 characters."), 
        Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Can't be null"), StringLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Can't be null"), Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        
        [Required(ErrorMessage = "Can't be null")]
        public int CategoryId { get; set; }
    }
}
