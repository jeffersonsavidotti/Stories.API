using Stories.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IStoryService
    {
        Task<StoryDTO> CreateStoryAsync(StoryDTO storyDto);
        Task<StoryDTO> GetStoryByIdAsync(int id);
        Task<IEnumerable<StoryDTO>> GetAllStoriesAsync();
        Task<StoryDTO> UpdateStoryAsync(int id, StoryDTO storyDto);
        Task<bool> DeleteStoryAsync(int id);
    }
}
