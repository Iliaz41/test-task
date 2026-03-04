using AutoMapper;
using Fitness.Application.DTOs.UsersDTO;
using Fitness.Application.IRepositories;
using Fitness.Application.IServices;
using Fitness.Domain.Exceptions;
using Fitness.Domain.Models;

namespace Fitness.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO, CancellationToken tokenCancel)
        {
            var user = _mapper.Map<User>(createUserDTO);
            var existingUser = await _userRepository.GetUserByEmailAsync(email: user.Email, token: tokenCancel, trackEntity: false);

            if (existingUser != null && user.Username == existingUser.Username)
            {
                throw new BadRequestException($"User with username {user.Username} is already exist");
            }

            await _userRepository.CreateAsync(entity: user, token: tokenCancel);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(CancellationToken tokenCancel)
        {
            var users = await _userRepository.GetAllAsync(token: tokenCancel);
            var usersDTOs = _mapper.Map<List<UserDTO>>(users);

            return usersDTOs;
        }

        public async Task<UserDTO> UpdateUserAsync(long id, UpdateUserDTO userDTO, CancellationToken tokenCancel)
        {
            if (userDTO.Id != id)
            {
                throw new BadRequestException("Id mismatch");
            }

            var user = await _userRepository.GetByIdAsync(id: userDTO.Id, token: tokenCancel)
                ?? throw new NotFoundException($"User with id {userDTO.Id} is not found.");

            user = _mapper.Map(userDTO, user);
            await _userRepository.Update(entity: user, token: tokenCancel);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUserAsync(long id, CancellationToken tokenCancel)
        {
            var user = await _userRepository.GetByIdAsync(id: id, token: tokenCancel)
                ?? throw new NotFoundException($"User with Id ({id}) is not found");

            await _userRepository.Delete(user, tokenCancel);
        }

        public async Task<UserDTO?> GetUserByIdAsync(long id, CancellationToken tokenCancel)
        {
            var user = await _userRepository.GetByIdAsync(id: id, token: tokenCancel)
                ?? throw new NotFoundException($"User with Id ({id}) is not found.");

            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }
    }
}
