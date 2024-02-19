using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryDTO>>> GetAllStories()
        {
            var stories = await _storyService.GetAllAsync();
            return Ok(stories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoryDTO>> GetStoryById(int id)
        {
            var story = await _storyService.GetByIdAsync(id);
            if (story == null)
            {
                return NotFound();
            }
            return Ok(story);
        }

        [HttpPost]
        public async Task<ActionResult<StoryDTO>> CreateStory([FromBody] StoryDTO storyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _storyService.AddStoryAsync(storyDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStory(int id, [FromBody] StoryDTO storyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _storyService.UpdateStoryAsync(id, storyDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var result = await _storyService.DeleteStoryAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}