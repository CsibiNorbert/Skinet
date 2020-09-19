using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skiner.Data.Contexts;
using Skinet.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _StoreContext;

        public ProductsController(StoreContext storeContext)
        {
            _StoreContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _StoreContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _StoreContext.Products.FindAsync(productId);
            return Ok(product);
        }
    }
}
