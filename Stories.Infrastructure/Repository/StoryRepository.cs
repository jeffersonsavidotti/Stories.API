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
    public class StoryRepository : IStoryRepository
    {
        private readonly AppDbContext _context;

        public StoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Story>> GetAllAsync()
        {
            return await _context.Stories.Include(s => s.Votes).ToListAsync();
        }

        public async Task<Story> GetByIdAsync(int id)
        {
            return await _context.Stories.Include(s => s.Votes).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddStoryAsync(Story story)
        {
            await _context.Stories.AddAsync(story);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStoryAsync(Story story)
        {
            _context.Stories.Update(story);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStoryAsync(int id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story != null)
            {
                _context.Stories.Remove(story);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
