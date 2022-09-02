using Microsoft.EntityFrameworkCore;
using goodsstore_backend.Models;

namespace goodsstore_backend.EFCore.Repositories.Interfaces
{
    public interface IGoodsStoreDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<OrderElement> OrdersElements { get; set; }
    }
}
