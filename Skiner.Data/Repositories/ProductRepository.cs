using Microsoft.EntityFrameworkCore;
using Skiner.Data.Contexts;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skiner.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _StoreContext;

        public ProductRepository(StoreContext storeContext)
        {
            _StoreContext = storeContext;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _StoreContext.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _StoreContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            // EF Eager loading (We include the FKs)
            return await _StoreContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
           return await _StoreContext.ProductTypes.ToListAsync();
        }
    }
}
