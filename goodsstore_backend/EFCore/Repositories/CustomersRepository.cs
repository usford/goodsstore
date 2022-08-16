using goodsstore_backend.Models;
using goodsstore_backend.EFCore;
using goodsstore_backend.EFCore.Repositories.Interfaces;


namespace goodsstore_backend.EFCore.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly GoodsStoreDbContext _dbContext;

        public CustomersRepository(GoodsStoreDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers;
        }
    }
}
