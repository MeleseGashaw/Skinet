using Microsoft.AspNetCore.Mvc;
using Core.Entities;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
_context=context;
        }
        [HttpGet]
        public  async Task<ActionResult<List<Product>>> GetProducts()
            {
                var products= await _context.Products.ToListAsync();
                return Ok(products);
            } 
             [HttpGet("{id}")] 
        public async Task<ActionResult<List<Product>>> GetProduct(int id)
            {
                return Ok(await _context.Products.FindAsync(id));
            }

    }
}