using Microsoft.EntityFrameworkCore;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_tests
{
    public class GoodsStoreDbContextTest : DbContext, IGoodsStoreDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderElement> OrdersElements { get; set; }

        public GoodsStoreDbContextTest(DbContextOptions<GoodsStoreDbContextTest> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GoodsStoreTest;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var customers = new List<Customer>();

            customers.Add(new Customer("Max", "1337-2022") 
            { 
                Id = new Guid("ab87cfa0-abaf-4420-acfd-ecc9fb058427") 
            });

            modelBuilder.Entity<Customer>()
                .HasData(customers);
        }
    }
}
