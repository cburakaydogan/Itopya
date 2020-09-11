using System.Collections.Generic;
using System.Threading.Tasks;
using Itopya.Application.Models.CategoryBundle;
using Itopya.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Itopya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryBundleController : ControllerBase
    {
        private readonly ICategoryBundleService _service;
        public CategoryBundleController(ICategoryBundleService service)
        {
            _service = service;
        }
         /// <summary>
        /// Add A Category In Bundle
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A category</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If category model is null or category already</response>
        [HttpPost(Name = "AddCategoryBundle")]
        [ProducesResponseType(201, Type = typeof(CategoryBundleAddDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryBundleAddDto model)
         {
            if (model == null)
            {
                return BadRequest("model is null");
            }
            var category = await _service.AddCategory(model);

            return CreatedAtRoute("GetCategoryBundle", new { id = category.CategoryId }, category);
         }

        /// <summary>
        /// Delete A Category In Bundle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Deleted successfully</response>
        /// <response code="404">Category not found</response>
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [HttpDelete("{id}", Name = "DeleteCategoryBundle")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _service.DeleteCategory(id))
            {
                return NotFound("Category Not Found");
            }
            return NoContent();
        }

        /// <summary>
        /// Get CategoryBundle
        /// </summary>
        /// <returns>List of Categories</returns>
        /// <response code="200">Returns CategoryBundle list</response>
        [ProducesResponseType(typeof(List<CategoryBundleDto>), 200)]
        [HttpGet(Name = "GetCategoryBundles")]
        public async Task<IActionResult> GetCategoryBundles()
        {
            var categories = await _service.GetCategoryBundles();

            return Ok(categories);
        }
        /// <summary>
        /// Get a Category In Bundle
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category by Id</returns>
        /// <response code="200">Returns a category</response>
        /// <response code="404">If category not found</response>
        [ProducesResponseType(200, Type = typeof(CategoryBundleDto))]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetCategoryBundle")]
        public async Task<IActionResult> GetCategoryBundle(int id)
        {
            var category = await _service.GetCategory(id);

            return Ok(category);
        }

    }
}
