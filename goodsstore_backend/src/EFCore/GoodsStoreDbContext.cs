using Microsoft.EntityFrameworkCore;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.EFCore
{
    public class GoodsStoreDbContext : DbContext, IGoodsStoreDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderElement> OrdersElements { get; set; }

        public GoodsStoreDbContext(DbContextOptions<GoodsStoreDbContext> options) : base(options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
