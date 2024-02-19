using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                // Mapeie outras propriedades conforme necessário
            });

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                // Mapeie outras propriedades conforme necessário
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                // Mapeie outras propriedades de UserDTO para User
            };

            await _userService.AddUserAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            var userToUpdate = await _userService.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Name = userDto.Name;
            // Mapeie outras propriedades de UserDTO para User

            var result = await _userService.UpdateUserAsync(userToUpdate);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
