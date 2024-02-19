using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IStoryService
    {
        Task<IEnumerable<StoryDTO>> GetAllAsync();
        Task<StoryDTO> GetByIdAsync(int id);
        Task AddStoryAsync(StoryDTO storyDTO);
        Task UpdateStoryAsync(int id, StoryDTO storyDTO);
        Task<bool> DeleteStoryAsync(int id);
    }
}
