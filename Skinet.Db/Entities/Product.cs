using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skinet.Core.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        // These are navigation props
        // FK to this table
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        // These are navigation props
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}
