using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http ;
namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {
      private readonly IGenericRepository<Product> _productsRepo;
      private readonly IGenericRepository<ProductBrand> _productBrandRepo;
      private readonly IGenericRepository<ProductType> _productTypeRepo;
      private IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {   
            _mapper=mapper; 
            _productsRepo=productsRepo;
            _productBrandRepo=productBrandRepo;
            _productTypeRepo=productTypeRepo;
        }
         [HttpGet("brands")]
        public  async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
            {
               return Ok(await _productBrandRepo.ListAllAsync());
   
            } 
        [HttpGet("types")]
        public  async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
            {
                return Ok(await _productTypeRepo.ListAllAsync());
                
            } 
                    [HttpGet]
        public  async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
            {
                var spec =new ProductsWithTypeAndBrandsSpecification();
                 var products= await _productsRepo.ListAsync(spec);
               // var products= await _productsRepo.ListAllAsync();//Old Methode
               return Ok(_mapper
               .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
               /* return Ok(products.Select(product=>new ProductToReturnDto
                
                 {
                    Id=product.Id,
                    Name=product.Name,
                    Description=product.Description,
                    PictureUrl=product.PictureUrl,
                    Price=product.Price,
                    ProductBrand=product.ProductBrand.Name,
                    ProuctType=product.ProuctType.Name,


              }
                ).ToList());*/
            } 
             [HttpGet("{id}")] 
             [ProducesResponseType(StatusCodes.Status200OK)]
             [ProducesResponseType(typeof (ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProduct(int id)
            {
                  var spec =new ProductsWithTypeAndBrandsSpecification(id);
                  var product =await _productsRepo.GetEntityWithSpec(spec);
                  if(product==null) return NotFound(new ApiResponse(404));
                  return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
             /* return Ok( new ProductToReturnDto
              {
                    Id=product.Id,
                    Name=product.Name,
                    Description=product.Description,
                    PictureUrl=product.PictureUrl,
                    Price=product.Price,
                    ProductBrand=product.ProductBrand.Name,
                    ProuctType=product.ProuctType.Name,


              });*/
              //  return Ok(await _productsRepo.GetByIDAsync(id));
            }

            
    }
}