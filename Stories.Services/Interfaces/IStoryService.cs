using Stories.Infrastructure.Models;

namespace Stories.Services.Interfaces
{
    public interface IStoryService
    {
        Task<IEnumerable<Story>> GetAllAsync();
        Task<Story> GetByIdAsync(int id);
        Task AddStoryAsync(Story story);
        Task<bool> UpdateStoryAsync(int id, Story story);
        Task<bool> DeleteStoryAsync(int id);
    }
}
