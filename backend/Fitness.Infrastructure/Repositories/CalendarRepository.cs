using Fitness.Application.IRepositories;
using Fitness.Domain.Models;
using Fitness.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories
{
    public class CalendarRepository : BaseRepository<Calendar>, ICalendarRepository
    {
        public CalendarRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<Calendar?> GetCalendarGuidAsync(Guid guid, CancellationToken tokenCancel,
            bool trackEntity = false, bool includeEntity = false)
        {
            tokenCancel.ThrowIfCancellationRequested();

            var query = _context.Calendars.AsQueryable();

            if (!trackEntity)
            {
                query = query.AsNoTracking();
            }

            if (includeEntity)
            {
                query = query.Include(c => c.Exercises)
                             .ThenInclude(e => e.MeasurementUnit)
                             .Include(c => c.Exercises)
                             .ThenInclude(e => e.Status);
            }

            return await query.FirstOrDefaultAsync(a => a.Guid == guid, tokenCancel);
        }
    }
}
