using Microsoft.EntityFrameworkCore;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore.Repositories.Interfaces;

namespace goodsstore_backend.EFCore.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        public ItemsRepository(IGoodsStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly IGoodsStoreDbContext _dbContext;

        public async Task<IEnumerable<Item>> Get()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<Item?> Get(Guid itemId)
        {
            Item? item = await _dbContext.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            return item;
        }
  
        public void Add(Item item)
        {
            _dbContext.Items.Add(item);
        }

        public async Task<Item?> Update(Item item)
        {
            bool check = await _dbContext.Items.AnyAsync(x => x.Id == item.Id);

            if (check)
            {
                _dbContext.Items.Update(item);
                return item;
            }

            return null;
        }

        public async Task<Item?> Remove(Guid itemId)
        {
            Item? item = await _dbContext.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            if (item is not null)
            {
                _dbContext.Items.Remove(item);
            }

            return item;
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
