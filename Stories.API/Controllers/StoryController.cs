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
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        // GET: api/Story
        [HttpGet]
        public async Task<IActionResult> GetAllStories()
        {
            var storyDtos = await _storyService.GetAllStoriesAsync();
            var viewModels = storyDtos.Select(dto => new StoryViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Department = dto.Department,
                PositiveVotesCount = dto.PositiveVotesCount, // Ajustado para incluir contagens de votos
                NegativeVotesCount = dto.NegativeVotesCount,
            }).ToList();

            return Ok(viewModels);
        }

        // GET: api/Story/5
        [HttpGet("{id}")]
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

        // POST: api/Story
        [HttpPost]
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
                // A criação não envolve diretamente contagens de votos
            };

            var createdStoryDto = await _storyService.CreateStoryAsync(storyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStoryDto.Id }, createdStoryDto);
        }

        // PUT: api/Story/5
        [HttpPut("{id}")]
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
                // As contagens de votos são gerenciadas separadamente
            };

            var updatedStoryDto = await _storyService.UpdateStoryAsync(id, storyDto);
            if (updatedStoryDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Story/5
        [HttpDelete("{id}")]
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
