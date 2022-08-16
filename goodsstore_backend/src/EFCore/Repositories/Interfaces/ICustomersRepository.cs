using goodsstore_backend.Models;

namespace goodsstore_backend.EFCore.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<Customer> GetAll();
        Customer? SingleOrDefault(Guid customerId);
        void Add(Customer customer);
        Customer? Remove(Guid customerId);
        Customer? Update(Customer customer);
    }
}
