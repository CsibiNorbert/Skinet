using Microsoft.Extensions.Logging;
using Skiner.Data.Contexts;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skiner.Infrastructure.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
			try
			{
				// Any checks for any records
				if (!storeContext.ProductBrands.Any())
				{
					var brandsData = File.ReadAllText("../Skiner.Data/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					foreach (var brand in brands)
					{
						storeContext.ProductBrands.Add(brand);
					}

					await storeContext.SaveChangesAsync();
				}

				if (!storeContext.ProductTypes.Any())
				{
					var typesData = File.ReadAllText("../Skiner.Data/SeedData/types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

					foreach (var type in types)
					{
						storeContext.ProductTypes.Add(type);
					}

					await storeContext.SaveChangesAsync();
				}

				if (!storeContext.Products.Any())
				{
					var productsData = File.ReadAllText("../Skiner.Data/SeedData/products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productsData);

					foreach (var prod in products)
					{
						storeContext.Products.Add(prod);
					}

					await storeContext.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<StoreContextSeed>();
				logger.LogError(ex.Message);
			}
        }
    }
}
