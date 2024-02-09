using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IStoryService
    {
        List<StoryDTO> GetAll();
        StoryDTO GetById(int id);
        void AddStory(int id, StoryDTO story);
        bool UpdateStory(int id, StoryDTO story);
        bool DeleteStory(int id);

        //
        //Task<IEnumerable<Story>> GetAllAsync();
        //Task<Story> GetByIdAsync(int id);
        //Task AddStoryAsync(Story story);
        //Task<bool> UpdateStoryAsync(int id, Story story);
        //Task<bool> DeleteStoryAsync(int id);
    }
}
