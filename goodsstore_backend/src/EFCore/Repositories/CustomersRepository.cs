using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using goodsstore_backend.Models;
using goodsstore_backend.EFCore;
using goodsstore_backend.EFCore.Repositories.Interfaces;


namespace goodsstore_backend.EFCore.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IGoodsStoreDbContext _dbContext;

        public CustomersRepository(IGoodsStoreDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await _dbContext.Customers.ToListAsync();
        }
        public async Task<Customer?> Get(Guid customerId)
        {
            Customer? customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);

            return customer;
        }
        public void Add(Customer customer)
        { 
            _dbContext.Customers.Add(customer);
        }
        public async Task<Customer?> Update(Customer customer)
        {
            bool check = await _dbContext.Customers.AnyAsync(x => x.Id == customer.Id);

            if (check)
            {
                _dbContext.Customers.Update(customer);
                return customer;
            }

            return null;
        }
        public async Task<Customer?> Remove(Guid customerId)
        {
            Customer? customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);

            if (customer is not null)
            {
                _dbContext.Customers.Remove(customer);
            }

            return customer;
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
