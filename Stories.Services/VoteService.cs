using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
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

        public async Task<Vote> GetVoteByStoryAndUserAsync(int storyId, int userId)
        {
            return await _context.Votes.FirstOrDefaultAsync(v => v.IdStory == storyId && v.IdUser == userId);
        }

        public async Task<bool> AddVoteAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateVoteAsync(Vote vote)
        {
            _context.Votes.Update(vote);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
