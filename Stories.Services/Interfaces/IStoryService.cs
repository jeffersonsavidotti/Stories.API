using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IStoryService
    {
        Task<StoryDTO> CreateStoryAsync(StoryDTO storyDto);
        Task<StoryDTO> GetStoryByIdAsync(Guid id);
        Task<IEnumerable<StoryDTO>> GetAllStoriesAsync();
        Task<StoryDTO> UpdateStoryAsync(Guid id, StoryDTO storyDto);
        Task<bool> DeleteStoryAsync(Guid id);
    }
}
