using Microsoft.AspNetCore.Mvc;
using Stories.Services;
using Stories.Services.DTOs;

[Route("api/[controller]")]
[ApiController]
public class StoriesController : ControllerBase
{
    private readonly IServices _storyService;

    public StoriesController(IServices storyService)
    {
        _storyService = storyService;
    }

    //GetAll
    [HttpGet]
    public async Task<IActionResult> GetAllStories()
    {
        var stories = await _storyService.GetAllStoriesAsync();
        return Ok(stories);
    }
    //GetID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStory(int id)
    {
        var story = await _storyService.GetStoryByIdAsync(id);
        if (story == null) return NotFound();
        return Ok(story);
    }
    //Post
    [HttpPost]
    public async Task<IActionResult> CreateStory([FromBody] StoryDTO storyDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdStory = await _storyService.AddStoryAsync(storyDto);
        return CreatedAtAction(nameof(GetStory), new { id = createdStory.Id }, createdStory);
    }
    ////Put
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateStory(int id, [FromBody] StoryDTO storyDto)
    //{
    //    if (!ModelState.IsValid) return BadRequest(ModelState);
    //    var result = await _storyService.UpdateStoryAsync(id, storyDto);
    //    if (!result) return NotFound();
    //    return NoContent();
    //}
    ////Delete
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteStory(int id)
    //{
    //    var result = await _storyService.DeleteStoryAsync(id);
    //    if (!result) return NotFound();
    //    return NoContent();
    //}
}

