using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Entities
{
    public class CustomerBasket
    {
        // Empty constructor for Redis
        public CustomerBasket()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Passing the Id of the basket from the UI</param>
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
