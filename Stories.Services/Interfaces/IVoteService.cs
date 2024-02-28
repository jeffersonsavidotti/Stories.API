using Stories.Services.DTOs;

namespace Stories.Services.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDTO> CreateVoteAsync(VoteDTO voteDto);
        Task<IEnumerable<VoteDTO>> GetAllVotesAsync();
    }
}
