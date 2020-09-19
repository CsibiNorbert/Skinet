using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skinet.Db.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        public string ProductName { get; set; }
    }
}
