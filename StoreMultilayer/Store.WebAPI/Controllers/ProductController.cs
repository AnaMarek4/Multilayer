using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.Model;
using Store.Service;
using Store.Common;

namespace Store.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductService productService;

        public ProductController(IConfiguration configuration)
        {
            this.productService = new ProductService(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("products/")]
        public async Task<IActionResult> Get(Guid? productId = null, string name = "", double? minPrice = null, double? maxPrice = null, string orderBy = "Price", string sortOrder = "DESC", int rpp = 10, int pageNumber = 1)
        {
            try
            {
                ProductFilter filter = new ProductFilter(productId, name, minPrice, maxPrice);
                OrderByFilter order = new OrderByFilter(orderBy, sortOrder);
                PageFilter page = new PageFilter(rpp, pageNumber);
                ICollection<Product> products = await productService.GetAsync(filter, order, page);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("products")]
        public async Task<IActionResult> Post(Product product)
        {
            if(product == null)
            {
                return BadRequest("The product field is required.");
            }
            try
            {
                int commitNumber = await productService.PostAsync(product);

                if(commitNumber == 0)
                {
                    return BadRequest("Failed to add the product.");
                }
                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("products/{productId:guid}")]

        public async Task<IActionResult> Put(Product product, Guid productId)
        {
            if(product == null)
            {
                return BadRequest();
            }
            try
            {
                int commitNumber = await productService.PutAsync(product, productId);
                if (commitNumber == 0)
                {
                    return BadRequest();
                }
                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpDelete("products/{productId:guid}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            try
            {
                int commitNumber = await productService.DeleteAsync(productId);
                if (commitNumber == 0)
                {
                    return BadRequest();
                }
                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
