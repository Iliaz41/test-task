using Fitness.Application.IRepositories;
using Fitness.Domain.Models;
using Fitness.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> CreateAsync(T entity, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await _dbSet.AddAsync(entity, token);
            await SaveAsync(token);

            return entity;
        }

        public async Task Delete(T entity, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            _dbSet.Remove(entity);
            await SaveAsync(token);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return await _dbSet.AsNoTracking().ToListAsync(token);
        }

        public async Task<T?> GetByIdAsync(long id, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<T?> GetByGuidAsync(Guid guid, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid, token);
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public async Task Update(T entity, CancellationToken token)
        {
            await SaveAsync(token);
        }
    }
}
