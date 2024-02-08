using Stories.Infrastructure.Models;

namespace Stories.Services.Interfaces
{
    internal interface IVoteService
    {
        Task<Vote> GetVoteByStoryAndUserAsync(int storyId, int userId);
        Task<bool> AddVoteAsync(Vote vote);
        Task<bool> UpdateVoteAsync(Vote vote);
    }
}
