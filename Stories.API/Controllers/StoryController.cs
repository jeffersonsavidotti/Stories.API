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
                VotesCount = dto.VotesCount,
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
                VotesCount = storyDto.VotesCount
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
                // VotesCount não é usado na criação
            };

            var createdStoryDto = await _storyService.CreateStoryAsync(storyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStoryDto.Id }, createdStoryDto);
        }

        // PUT: api/Story/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StoryViewModel viewModel)
        {
            if (id != viewModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var storyDto = new StoryDTO
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Department = viewModel.Department,
                // VotesCount é gerenciado separadamente e não é atualizado aqui
            };

            var success = await _storyService.UpdateStoryAsync(id, storyDto);
            if (success == null)
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
