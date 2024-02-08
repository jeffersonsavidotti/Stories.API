using Microsoft.AspNetCore.Mvc;
using Stories.Infrastructure.Models;
using Stories.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class StoriesController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoriesController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    //GetAll
    [HttpGet]
    public async Task<IActionResult> GetAllStories()
    {
        var stories = await _storyService.GetAllAsync();
        return Ok(stories);
    }
    //GetID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStory(int id)
    {
        var story = await _storyService.GetByIdAsync(id);
        if (story == null) return NotFound();
        return Ok(story);
    }
    //Post
    [HttpPost]
    public async Task<IActionResult> CreateStory([FromBody]Story story)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _storyService.AddStoryAsync(story);
        return Ok(story);
        
    }
    //Put
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStory(int id, Story story)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _storyService.UpdateStoryAsync(id, story);
        if (!result) return NotFound();
        return NoContent();
    }
    //Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStory(int id)
    {
        var result = await _storyService.DeleteStoryAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}

