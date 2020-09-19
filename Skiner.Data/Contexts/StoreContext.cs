using Microsoft.EntityFrameworkCore;
using Skinet.Db.Entities;

namespace Skiner.Data.Contexts
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
