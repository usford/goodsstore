using Microsoft.EntityFrameworkCore;
using goodsstore_backend.Models;

namespace goodsstore_backend.EFCore
{
    public class GoodsStoreDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderElement> OrdersElements { get; set; }

        public GoodsStoreDbContext(DbContextOptions<GoodsStoreDbContext> options) : base(options) 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
    }
}
