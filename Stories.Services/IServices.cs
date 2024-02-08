using Stories.Services.DTOs;

namespace Stories.Services
{
    public interface IServices
    {
        Task<IEnumerable<StoryDTO>> GetAllStoriesAsync();
        Task<StoryDTO> GetStoryByIdAsync(int id);
        Task<StoryDTO> AddStoryAsync(StoryDTO storyDto);
        //Task<bool> UpdateStoryAsync(int id, StoryDTO storyDto);
        //Task<StoryDTO> DeleteStoryAsync(int id);
        //Task<bool> VoteStoryAsync(int storyId, int userId, bool isLike);
    }
}
