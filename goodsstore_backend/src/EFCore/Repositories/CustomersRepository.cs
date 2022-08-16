using Microsoft.EntityFrameworkCore;
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
        public Customer? SingleOrDefault(Guid customerId)
        {
            Customer? customer = _dbContext.Customers.SingleOrDefault(x => x.Id == customerId);

            return customer;
        }
        public void Add(Customer customer)
        { 
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }
        public Customer? Remove(Guid customerId)
        {
            var customer = _dbContext.Customers.SingleOrDefault(x => x.Id == customerId);

            if (customer is not null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }

            return customer;
        }
        public Customer? Update(Customer customer)
        {
            bool check = _dbContext.Customers.Any(x => x.Id == customer.Id);

            if (check)
            {
                _dbContext.Customers.Update(customer);
                _dbContext.SaveChanges();
                return customer;
            }

            return null;
        }
    }
}
