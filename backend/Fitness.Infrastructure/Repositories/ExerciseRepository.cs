using Fitness.Application.IRepositories;
using Fitness.Domain.Models;
using Fitness.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ApplicationDbContext context) : base(context) 
        { }

        public async Task<Exercise?> GetExerciseGuidAsync(Guid guid, CancellationToken tokenCancel,
            bool trackEntity = false, bool includeEntity = false)
        {
            tokenCancel.ThrowIfCancellationRequested();

            var query = _context.Exercises.AsQueryable();

            if (!trackEntity)
            {
                query = query.AsNoTracking();
            }

            if (includeEntity)
            {
                query = query.Include(e => e.User)
                             .Include(e => e.Calendar);
            }

            return await query.FirstOrDefaultAsync(a => a.Guid == guid, tokenCancel);
        }

        public async Task<List<Exercise>> GetUserExercisesForDayAsync(long userId, DateTime day, CancellationToken tokenCancel)
        {
            tokenCancel.ThrowIfCancellationRequested();

            var query = _context.Exercises.AsQueryable();

            query = query.AsNoTracking();
            query = query.Include(e => e.MeasurementUnit)
                         .Include(e => e.Status)
                         .Where(e => e.UserId == userId && e.Calendar.Day.Date == day.Date);

            return await query.ToListAsync(tokenCancel);
        }
    }
}
