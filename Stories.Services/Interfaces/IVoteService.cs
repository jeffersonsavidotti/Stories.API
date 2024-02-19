using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IVoteService
    {
        Task<Vote> GetVoteByStoryAsync(int storyId);
        Task<Vote> GetVoteByUserAsync(int userId);
        Task<bool> AddVoteAsync(VoteDTO voteDTO);
        Task<bool> UpdateVoteAsync(VoteDTO voteDTO);
    }
}
