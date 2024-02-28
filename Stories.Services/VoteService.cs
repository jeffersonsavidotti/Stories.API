using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.Services
{
    public class VoteService : IVoteService
    {
        private readonly AppDbContext _context;

        public VoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VoteDTO> CreateVoteAsync(VoteDTO voteDto)
        {
            var vote = new Vote
            {
                IdStory = voteDto.IdStory,
                IdUser = voteDto.IdUser,
                VoteValue = voteDto.VoteValue
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            var story = await _context.Stories.FindAsync(voteDto.IdStory);
            if (story != null)
            {
                if (vote.VoteValue)
                {
                    story.PositiveVotesCount++;
                }
                else
                {
                    story.NegativeVotesCount++;
                }
                await _context.SaveChangesAsync();
            }
            return voteDto;
        }


        public async Task<IEnumerable<VoteDTO>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Select(v => new VoteDTO
                {
                    IdStory = v.IdStory,
                    IdUser = v.IdUser,
                    VoteValue = v.VoteValue
                })
                .ToListAsync();

            return votes;
        }

    }
}
