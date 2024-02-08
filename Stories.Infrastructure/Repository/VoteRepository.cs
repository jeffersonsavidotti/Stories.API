using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Data;
using Stories.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.Votes.FirstOrDefaultAsync(v => v.StoryId == storyId && v.UserId == userId);
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
