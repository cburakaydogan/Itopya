using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Itopya.Application.Models.Category;
using Itopya.Application.Services.Abstract;
using Itopya.Domain.Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Itopya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create A Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Created category</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If category is null</response>
        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(201, Type = typeof(CategoryCreateDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto model)
        {
            if (model == null)
            {
                return BadRequest("model is null");
            }
            var category = await _service.CreateCategory(model);

            return CreatedAtRoute("GetCategory", new { category.Id }, category);
        }

        /// <summary>
        /// Update a Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Updated successfully</response>
        /// <response code="400">If the category is null or category ids not same</response>
        /// <response code="404">If category is not found</response>
        [HttpPut("{id}", Name = "PutCategory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest("Model is null");
            }

            if (!await _service.UpdateCategory(model))
            {
                return NotFound("Record not found");
            }

            return NoContent();
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Deleted successfully</response>
        /// <response code="404">Category not found</response>
        /// 
        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _service.DeleteCategory(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Get A Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category by id</returns>
        /// <response code="200">Returns a category</response>
        /// <response code="404">If category not found</response>
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _service.GetCategory(id);

            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        /// <summary>
        /// Get List Categories
        /// </summary>
        /// <param name="categoryParameters"></param>
        /// <returns>List of Categories</returns>
        /// <response code="200">Returns category list</response>
        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParameters categoryParameters, [FromServices] IMapper mapper)
        {
            var categories = await _service.GetAllCategories(categoryParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(categories.MetaData));

            var categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

    }
}
