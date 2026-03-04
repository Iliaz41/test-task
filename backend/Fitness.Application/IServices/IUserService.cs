using Fitness.Application.DTOs.UsersDTO;

namespace Fitness.Application.IServices
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO, CancellationToken tokenCancel);
        Task<List<UserDTO?>> GetAllUsersAsync(CancellationToken tokenCancel);
        Task<UserDTO> UpdateUserAsync(long id, UpdateUserDTO userDTO, CancellationToken tokenCancel);
        Task DeleteUserAsync(long id, CancellationToken tokenCancel);
        Task<UserDTO?> GetUserByIdAsync(long id, CancellationToken tokenCancel);
    }
}
