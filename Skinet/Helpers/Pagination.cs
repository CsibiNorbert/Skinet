using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Helpers
{
    // Generic Pagination
    public class Pagination<T> where T: class
    {
        public Pagination(
            int pageIndex, 
            int pageSize, 
            int count, 
            IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        // Count of objects after filter applied
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; } = null!;
    }
}
