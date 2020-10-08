using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Specifications
{
    /// <summary>
    /// This class is used to pass parameters inside our controler as a payload
    /// </summary>
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        
        // By default will return the very first page
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        // Pagesize is used for skiping the size of a page when going through the products
        public int PageSize {
            get => _pageSize;
            set
            {
                // stops from returning more than 50 results in a single result
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }

        // Search
        private string? _search;
        public string? Search {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
