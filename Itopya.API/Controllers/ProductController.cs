using System.Collections.Generic;
using System.Threading.Tasks;
using Itopya.Application.Models.Product;
using Itopya.Application.Services.Abstract;
using Itopya.Domain.Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Itopya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        /// <summary>
        /// Create a Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return created product</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If product is null</response>
        [ProducesResponseType(201, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto model)
        {
            if (model == null)
            {
                return BadRequest("model is null");
            }
            var product = await _service.CreateProduct(model);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Updated successfully</response>
        /// <response code="400">If the product is null or ids not same</response>
        /// <response code="404">If product is not found</response>
        [HttpPut("{id}", Name = "PutProduct")]
        [ProducesResponseType(204, Type = typeof(ProductUpdateDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDto model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest("Model is null");
            }

            if (!await _service.UpdateProduct(model))
            {
                return NotFound("Record not found");
            }

            return NoContent();
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Deleted successfully</response>
        /// <response code="404">Product not found</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!await _service.DeleteProduct(id))
            {
                return NotFound("Record Not Found");
            }
            return NoContent();
        }

        /// <summary>
        /// Get a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product by Id</returns>
        /// <response code="200">Returns a product</response>
        /// <response code="404">If product not found</response>
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetProduct")]     
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _service.GetProduct(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <param name="productParameters"></param>
        /// <returns>List of products</returns>
        /// <response code="200">Returns product list</response>
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters productParameters)
        {
            var products = await _service.GetProducts(productParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return Ok(products);
        }
    }
}
