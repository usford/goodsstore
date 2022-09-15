using Microsoft.EntityFrameworkCore;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.EFCore.Repositories
{
    public class OrdersElementsRepository : IOrdersElementsRepository
    {
        public OrdersElementsRepository(IGoodsStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly IGoodsStoreDbContext _dbContext;

        public async Task<IEnumerable<OrderElement>> Get()
        {
            return await _dbContext.OrdersElements
                .Include(o => o.Order).ThenInclude(o => o.Customer)
                .Include(o => o.Item)
                .ToListAsync();
        }

        public async Task<OrderElement?> Get(Guid orderElementId)
        {
            OrderElement? orderElement = await _dbContext.OrdersElements
                .Include(o => o.Order).ThenInclude(o => o.Customer)
                .Include(o => o.Item)
                .SingleOrDefaultAsync(x => x.Id == orderElementId);

            return orderElement;
        }

        public void Add(OrderElement orderElement)
        {
            _dbContext.OrdersElements.Add(orderElement);
        }

        public async Task<OrderElement?> Update(OrderElement orderElement)
        {
            bool check = await _dbContext.OrdersElements.AnyAsync(x => x.Id == orderElement.Id);

            if (check)
            {
                _dbContext.OrdersElements.Update(orderElement);
                return orderElement;
            }

            return null;
        }

        public async Task<OrderElement?> Remove(Guid orderElementId) 
        {
            OrderElement? orderElement = await _dbContext.OrdersElements.SingleOrDefaultAsync(x => x.Id == orderElementId);

            if (orderElement is not null)
            {
                _dbContext.OrdersElements.Remove(orderElement);
            }

            return orderElement;
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
