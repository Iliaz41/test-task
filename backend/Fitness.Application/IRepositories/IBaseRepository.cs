namespace Fitness.Application.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken token);

        Task<T?> GetByIdAsync(long id, CancellationToken token);

        Task<T?> GetByGuidAsync(Guid guid, CancellationToken token);

        Task<T?> CreateAsync(T entity, CancellationToken token);

        Task Update(T entity, CancellationToken token);

        Task Delete(T entity, CancellationToken token);

        Task SaveAsync(CancellationToken token);
    }
}
