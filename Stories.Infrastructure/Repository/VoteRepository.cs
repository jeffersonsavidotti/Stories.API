using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;

namespace Stories.Infrastructure.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
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
