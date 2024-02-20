using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDTO> CreateVoteAsync(VoteDTO voteDto);
        Task<VoteDTO> GetVoteByIdAsync(int id);
        Task<IEnumerable<VoteDTO>> GetAllVotesAsync();
        Task<bool> DeleteVoteAsync(int id);
    }
}
