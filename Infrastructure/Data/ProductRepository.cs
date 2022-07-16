 
using System.Collections.Generic;
 using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using  Core.Interfaces;
using Core.Entities;

 
 

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
           
            _context=context;
        }
       public async   Task<Product> GetProductByIdAsync(int id)
         {
             return await _context.Products
                   .Include(p=>p.ProuctType)
        .Include(p=>p.ProductBrand)
             .FirstOrDefaultAsync(p=>p.Id==id);
         }
    public async Task<IReadOnlyList<Product>> GetProductsByAsync()
    {
        
       return await _context.Products
       .Include(p=>p.ProuctType)
        .Include(p=>p.ProductBrand)
       .ToListAsync();
    }
  
   public async Task<IReadOnlyList<ProductType>> GetProductTypesByAsync()
    {
       return await _context.ProductTypes.ToListAsync();
    }

     public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsByAsync()
    {
       return await _context.ProductBrands.ToListAsync();
    }
    }
}