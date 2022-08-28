using goodsstore_backend.EFCore.Repositories.Interfaces;
using goodsstore_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace goodsstore_backend.EFCore.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IGoodsStoreDbContext _dbContext;

        public OrdersRepository(IGoodsStoreDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            return await _dbContext.Orders.Include(o => o.Customer).ToListAsync();
        }
        public async Task<Order?> Get(Guid orderId)
        {
            Order? order = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == orderId);

            return order;
        }
        public void Add(Order order)
        {
            _dbContext.Orders.Add(order);
        }
        public async Task<Order?> Update(Order order)
        {
            bool check = await _dbContext.Orders.AnyAsync(x => x.Id == order.Id);

            if (check)
            {
                _dbContext.Orders.Update(order);
                return order;
            }

            return null;
        }
        public async Task<Order?> Remove(Guid orderId)
        {
            Order? order = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == orderId);

            if (order is not null)
            {
                _dbContext.Orders.Remove(order);
            }

            return order;
        }
        public async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            if (_dbContext is DbContext dbContext)
            {
                return await dbContext.SaveChangesAsync(token);
            }

            return 0;
        }       
    }
}
