using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.Model;
using Store.Service;

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

        [HttpGet("products")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ICollection<Product> products = await productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("products/{productId:guid}")]
        public async Task<IActionResult> Get(Guid productId)
        {
            try
            {
                Product product = await productService.Get(productId);
                return Ok(product);
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
                return BadRequest();
            }
            try
            {
                int commitNumber = await productService.Post(product);

                if(commitNumber == 0)
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

        [HttpPut("products/{productId:guid}")]

        public async Task<IActionResult> Put(Product product, Guid productId)
        {
            if(product == null)
            {
                return BadRequest();
            }
            try
            {
                int commitNumber = await productService.Put(product, productId);
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
                int commitNumber = await productService.Delete(productId);
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
