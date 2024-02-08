using Microsoft.AspNetCore.Mvc;
using Stories.Core.DTOs;
using Stories.Core.Interfaces;
using Stories.Services.DTOs;
using Stories.Services;
using System.Threading.Tasks;

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
    public async Task<IActionResult> GetAllStories()
    {
        var stories = await _storyService.GetAllStoriesAsync();
        return Ok(stories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStory(int id)
    {
        var story = await _storyService.GetStoryByIdAsync(id);
        if (story == null) return NotFound();
        return Ok(story);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStory([FromBody] StoryDTO storyDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdStory = await _storyService.AddStoryAsync(storyDto);
        return CreatedAtAction(nameof(GetStory), new { id = createdStory.Id }, createdStory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStory(int id, [FromBody] StoryDTO storyDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _storyService.UpdateStoryAsync(id, storyDto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStory(int id)
    {
        var result = await _storyService.DeleteStoryAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}

///
/// Esta estrutura básica cobre a criação de um serviço para gerenciar histórias, incluindo a interface de serviço, a implementação do serviço e o controlador ASP.NET Core para expor as operações via HTTP. Lembre-se de que a implementação real do StoryService, especialmente os métodos AddStoryAsync, UpdateStoryAsync, e DeleteStoryAsync, depende da sua lógica de negócios específica e de como você implementa o acesso aos dados através do repositório.
///
