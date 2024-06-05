using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.Model;
using Store.Service;
using Store.WebAPI;

namespace Store.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class StoreController : ControllerBase
    {
        private StoreService storeService;
        public StoreController(IConfiguration configuration)
        {
            this.storeService = new StoreService(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("stores")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ICollection<StoreM> stores = await storeService.GetAll();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
