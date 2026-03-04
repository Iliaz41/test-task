using Fitness.Application.IRepositories;
using Fitness.Domain.Models;
using Fitness.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token,
            bool trackEntity = false, bool includeEntity = true)
        {
            token.ThrowIfCancellationRequested();

            var query = _context.Users.AsQueryable();

            if (!trackEntity)
            {
                query = query.AsNoTracking();
            }

            if (includeEntity)
            {
                query = query.Include(u => u.Exercises)
                             .ThenInclude(e => e.MeasurementUnit)
                             .Include(u => u.Exercises)
                             .ThenInclude(e => e.Status);

            }

            return await query.FirstOrDefaultAsync(x => x.Email == email, token);
        }

        public async Task<User?> GetUserByIdWithExerciseAsync(long id, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var query = _context.Users.AsQueryable();

            query = query.AsNoTracking();
            query = query.Include(u => u.Exercises)
                         .ThenInclude(e => e.MeasurementUnit)
                         .Include(u => u.Exercises)
                         .ThenInclude(e => e.Status);

            return await query.FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
