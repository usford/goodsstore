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
            var orders = new List<Order>();
            var items = new List<Item>();
            var ordersElements = new List<OrderElement>();

            customers.Add(new Customer("Max", "1337-2022") 
            { 
                Id = new Guid("ab87cfa0-abaf-4420-acfd-ecc9fb058427") 
            });

            orders.Add(new Order(new Guid("ab87cfa0-abaf-4420-acfd-ecc9fb058427"), DateTime.Now)
            {
                Id = new Guid("7223681c-b1a1-4187-a3f5-b24211e2634e")
            });

            items.Add(new Item(code: "93-1884-YO19", name: "Dildo", price: 2000, category: "Игрушки")
            {
                Id = new Guid("10d11558-9161-435b-9187-8c5ff23eec72")
            });

            ordersElements.Add(new OrderElement(orderId: orders[0].Id, itemId: items[0].Id, 5, itemPrice: items[0].Price)
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            });

            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<Item>().HasData(items);
            modelBuilder.Entity<OrderElement>().HasData(ordersElements);
        }
    }
}
