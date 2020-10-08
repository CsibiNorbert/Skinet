using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Specifications
{
    /// <summary>
    /// This is used to get only the count of items
    /// </summary>
    public class ProductFiltersWithCountSpecification : BaseSpecification<Product>
    {
        public ProductFiltersWithCountSpecification(ProductSpecParams productSpecParams) : base (x =>
                (string.IsNullOrEmpty(productSpecParams.Search) || x.ProductName.ToLower().Contains(productSpecParams.Search)) &&
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {

        }
    }
}
