// using Microsoft.AspNetCore.Mvc;
// using Core.Entities;
// using Core.Interfaces;
// namespace API.Controllers
// {
//     [ApiController]
//     [Route("api/[Controller]")]
//     public class ProductsController : ControllerBase
//     {
//         private readonly IProductRepository _repo;
//         public ProductsController(IProductRepository repo)
//         {
//             _repo = repo;
//         }
//         [HttpGet]
//         public async Task<ActionResult<List<Product>>> GetProducts()
//         {
//             var products = await _repo.GetProductsByAsync();
//             return Ok(products);
//         }
//         [HttpGet("{id}")]
//         public async Task<ActionResult<List<Product>>> GetProduct(int id)
//         {
//             return Ok(await _repo.GetProductByIdAsync(id));
//         }

//         [HttpGet("brands")]
//         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
//         {
//             return Ok(await _repo.GetProductBrandsByAsync());

//         }
//         [HttpGet("types")]
//         public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
//         {
//             return Ok(await _repo.GetProductTypesByAsync());

//         }
//     }
// }