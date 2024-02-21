using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Stories.API.Controllers
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

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDtos = await _userService.GetAllUsersAsync();
            var viewModels = userDtos.Select(dto => new UserViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                // Atualizado para refletir a estrutura atual do UserDTO
                PositiveVotesCount = dto.PositiveVotesCount,
                NegativeVotesCount = dto.NegativeVotesCount
            }).ToList();

            return Ok(viewModels);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name,
                // Reflete a estrutura atual do UserDTO
                PositiveVotesCount = userDto.PositiveVotesCount,
                NegativeVotesCount = userDto.NegativeVotesCount
            };

            return Ok(viewModel);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = new UserDTO
            {
                Name = viewModel.Name
                // Informações sobre votos são gerenciadas separadamente
            };

            var createdUserDto = await _userService.CreateUserAsync(userDto);
            // Retorna o usuário criado com o status 201 Created
            return CreatedAtAction(nameof(GetById), new { id = createdUserDto.Id }, createdUserDto);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UserViewModel viewModel)
        {
            if (id != viewModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = new UserDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name
                // Informações sobre votos são gerenciadas separadamente
            };

            var updatedUserDto = await _userService.UpdateUserAsync(id, userDto);
            if (updatedUserDto == null)
            {
                return NotFound();
            }

            // Resposta NoContent (204) após a atualização bem-sucedida
            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); // Retorna 204 No Content para indicar sucesso na operação de delete
        }
    }
}