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

        public async Task<Vote> GetVoteByStoryAsync(int storyId)
        {
            return await _context.Votes.FirstOrDefaultAsync(v => v.IdStory == storyId);
        }

        public async Task<Vote> GetVoteByUserAsync(int userId)
        {
            return await _context.Votes.FirstOrDefaultAsync(v => v.IdUser == userId);
        }

        public async Task<bool> AddVoteAsync(VoteDTO voteDTO)
        {
            Vote vote = new Vote()
            {
                Id = voteDTO.Id,
                Story = voteDTO.Story,
                User = voteDTO.User
            };
            await _context.Votes.AddAsync(vote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateVoteAsync(VoteDTO voteDTO)
        {
            Vote vote = new Vote()
            {
                Id = voteDTO.Id,
                Story = voteDTO.Story,
                User = voteDTO.User
            };
            _context.Votes.Update(vote);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
