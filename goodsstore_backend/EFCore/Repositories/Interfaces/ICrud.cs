namespace goodsstore_backend.EFCore.Repositories.Interfaces
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T?> Get(Guid entityId);
        void Add(T entity);
        Task<T?> Update(T entity);
        Task<T?> Remove(Guid entityId);
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
