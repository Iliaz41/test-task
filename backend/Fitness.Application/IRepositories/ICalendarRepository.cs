using Fitness.Domain.Models;

namespace Fitness.Application.IRepositories
{
    public interface ICalendarRepository : IBaseRepository<Calendar>
    {
        Task<Calendar?> GetCalendarGuidAsync(Guid guid, CancellationToken tokenCancel, bool trackEntity = false, bool includeEntity = false);
    }
}
