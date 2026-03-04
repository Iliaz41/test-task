using Fitness.Application.DTOs.UsersDTO;
using Fitness.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateUserDTO>> Register([FromBody] CreateUserDTO createUserDTO, CancellationToken tokenCancel)
        {
            var userDto = await _userService.CreateUserAsync(createUserDTO, tokenCancel);

            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(CancellationToken tokenCancel)
        {
            return Ok(await _userService.GetAllUsersAsync(tokenCancel));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserDTO userDTO, CancellationToken tokenCancel)
        {
            return Ok(await _userService.UpdateUserAsync(id, userDTO, tokenCancel));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(long id, CancellationToken tokenCancel)
        {
            await _userService.DeleteUserAsync(id, tokenCancel);

            return NoContent();
        }

        [HttpGet]
        [Route("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(long id, CancellationToken tokenCancel)
        {
            return Ok(await _userService.GetUserByIdAsync(id, tokenCancel));
        }
    }
}
