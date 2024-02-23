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

            voteDto.Id = vote.Id;
            return voteDto;
        }


        public async Task<IEnumerable<VoteDTO>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Select(v => new VoteDTO
                {
                    Id = v.Id,
                    IdStory = v.IdStory,
                    IdUser = v.IdUser,
                    VoteValue = v.VoteValue
                })
                .ToListAsync();

            return votes;
        }

        public async Task<VoteDTO> GetVoteByIdAsync(int id)
        {
            var vote = await _context.Votes
                .Where(v => v.Id == id)
                .Select(v => new VoteDTO
                {
                    Id = v.Id,
                    IdStory = v.IdStory,
                    IdUser = v.IdUser,
                    VoteValue = v.VoteValue
                })
                .FirstOrDefaultAsync();

            return vote;
        }

        public async Task<bool> DeleteVoteAsync(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return false;
            }

            _context.Votes.Remove(vote);

            var story = await _context.Stories.FindAsync(vote.IdStory);
            if (story != null)
            {
                if (vote.VoteValue)
                {
                    story.PositiveVotesCount--;
                }
                else
                {
                    story.NegativeVotesCount--;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
