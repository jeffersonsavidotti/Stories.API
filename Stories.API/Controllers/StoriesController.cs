using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
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
    public IActionResult GetAll()
    {
        var story = _storyService.GetAll().ToList();
        if(story.Count == 0)
            return NoContent();

        return Ok(story);

    }

    //GetID
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var story = _storyService.GetAll().FirstOrDefault(x => x.Id == id);
        if (story.Id == null)
            return NotFound();

        return Ok(story);
    }
    //Post
    [HttpPost]
    public IActionResult AddStory( StoryDTO story)
    {
        var storyDTO = new StoryDTO();
        return Ok(storyDTO);
    }
    //Put
    [HttpPut("{id}")]
    public IActionResult UpdateStory(int id, StoryDTO story)
    {
        StoryDTO storyDTO = new StoryDTO();
        return Ok(storyDTO);

    }
    //Delete
    [HttpDelete("{id}")]
    public IActionResult DeleteStory(int id)
    {
        var result = _storyService.DeleteStory(id);
        if (!result) return NotFound();
        return NoContent();
    }
}

