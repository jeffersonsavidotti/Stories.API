using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.Interfaces;


namespace Stories.Services
{
    internal class StoryService : IStoryService
    {
        private readonly AppDbContext _context;

        public StoryService(AppDbContext context)
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

        public async Task<bool> UpdateStoryAsync(int id, Story story)
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
