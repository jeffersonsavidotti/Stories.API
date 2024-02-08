using Stories.Services.DTOs;

namespace Stories.Services
{
    public interface IServices
    {
        Task<IEnumerable<StoryDTO>> GetAllStoriesAsync();
        Task<bool> VoteStoryAsync(int storyId, int userId, bool isLike);
    }

    public interface IStoryService
    {
        Task<IEnumerable<StoryDTO>> GetAllStoriesAsync();
        Task<StoryDTO> GetStoryByIdAsync(int id);
        Task<StoryDTO> AddStoryAsync(StoryDTO storyDto);
        Task<bool> UpdateStoryAsync(int id, StoryDTO storyDto);
        Task<bool> DeleteStoryAsync(int id);
    }
}
}
