using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        /// <summary>
        /// Obtém todas as histórias.
        /// </summary>
        /// <response code="200">Lista de histórias retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), 200)]
        public async Task<IActionResult> GetAllStories()
        {
            var storyDtos = await _storyService.GetAllStoriesAsync();
            var viewModels = storyDtos.Select(dto => new StoryViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Department = dto.Department,
                PositiveVotesCount = dto.PositiveVotesCount,
                NegativeVotesCount = dto.NegativeVotesCount,
            }).ToList();

            return Ok(viewModels);
        }

        /// <summary>
        /// Obtém uma história por ID.
        /// </summary>
        /// <param name="id">ID da história.</param>
        /// <response code="200">História encontrada com sucesso.</response>
        /// <response code="404">História não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoryViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var storyDto = await _storyService.GetStoryByIdAsync(id);
            if (storyDto == null)
            {
                return NotFound();
            }

            var viewModel = new StoryViewModel
            {
                Id = storyDto.Id,
                Title = storyDto.Title,
                Description = storyDto.Description,
                Department = storyDto.Department,
                PositiveVotesCount = storyDto.PositiveVotesCount,
                NegativeVotesCount = storyDto.NegativeVotesCount,
            };

            return Ok(viewModel);
        }

        /// <summary>
        /// Cria uma nova história.
        /// </summary>
        /// <param name="viewModel">Dados da história a ser criada.</param>
        /// <response code="201">História criada com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(StoryDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] StoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storyDto = new StoryDTO
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Department = viewModel.Department,
            };

            var createdStoryDto = await _storyService.CreateStoryAsync(storyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStoryDto.Id }, createdStoryDto);
        }

        /// <summary>
        /// Atualiza uma história existente.
        /// </summary>
        /// <param name="id">ID da história a ser atualizada.</param>
        /// <param name="viewModel">Novos dados da história.</param>
        /// <response code="204">História atualizada com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos.</response>
        /// <response code="404">História não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Edit(int id, [FromBody] StoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storyDto = new StoryDTO
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Department = viewModel.Department,
            };

            var updatedStoryDto = await _storyService.UpdateStoryAsync(id, storyDto);
            if (updatedStoryDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui uma história existente.
        /// </summary>
        /// <param name="id">ID da história a ser excluída.</param>
        /// <response code="204">História excluída com sucesso.</response>
        /// <response code="404">História não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _storyService.DeleteStoryAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
