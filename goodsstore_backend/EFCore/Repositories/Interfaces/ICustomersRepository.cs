using goodsstore_backend.Models;

namespace goodsstore_backend.EFCore.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<Customer> GetAll();
    }
}
