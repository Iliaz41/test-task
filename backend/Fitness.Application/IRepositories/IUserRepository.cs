using Fitness.Domain.Models;

namespace Fitness.Application.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken token, bool trackEntity = false, bool includeEntity = false);
        Task<User?> GetUserByIdWithExerciseAsync(long id, CancellationToken token);
    }
}
