using Fitness.Domain.Models;

namespace Fitness.Application.IRepositories
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        Task<Exercise?> GetExerciseGuidAsync(Guid guid, CancellationToken tokenCancel, bool trackEntity = false, bool includeEntity = false);
        Task<List<Exercise>> GetUserExercisesForDayAsync(long userId, DateTime day, CancellationToken tokenCancel);
    }
}
