using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtem todos os usuários.
        /// </summary>
        /// <response code="200">Lista de usuários retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserViewModel>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDtos = await _userService.GetAllUsersAsync();
            var viewModels = userDtos.Select(dto => new UserViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            }).ToList();

            return Ok(viewModels);
        }

        /// <summary>
        /// Obtem um usuario por ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <response code="200">Usuário encontrado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            var viewModel = new UserViewModel
            {
                Id = userDto.Id,
                Name = userDto.Name
            };

            return Ok(viewModel);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="viewModel">Dados do usuário a ser criado.</param>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = new UserDTO
            {
                Name = viewModel.Name
            };

            var createdUserDto = await _userService.CreateUserAsync(userDto);

            return CreatedAtAction(nameof(GetById), new { id = createdUserDto.Id }, createdUserDto);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="viewModel">Novos dados do usuário.</param>
        /// <response code="204">Usuário atualizado com sucesso.</response>
        /// <response code="400">ID inválido ou dados inválidos fornecidos.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Edit(Guid id, [FromBody] UserViewModel viewModel)
        {
            if (id != viewModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = new UserDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };

            var updatedUserDto = await _userService.UpdateUserAsync(id, userDto);
            if (updatedUserDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário a ser excluído.</param>
        /// <response code="204">Usuário excluído com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
