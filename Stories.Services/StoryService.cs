using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.Services
{
    public class StoryService : IStoryService
    {
        private readonly AppDbContext _context;

        public StoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StoryDTO> CreateStoryAsync(StoryDTO storyDto)
        {
            var story = new Story
            {
                Title = storyDto.Title,
                Description = storyDto.Description,
                Department = storyDto.Department,
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            return new StoryDTO(story);
        }

        public async Task<StoryDTO> GetStoryByIdAsync(int id)
        {
            var story = await _context.Stories
                .Include(s => s.Votes)
                .FirstOrDefaultAsync(s => s.Id == id);

            return story != null ? new StoryDTO(story) : null;
        }

        public async Task<IEnumerable<StoryDTO>> GetAllStoriesAsync()
        {
            var stories = await _context.Stories
                .Include(s => s.Votes)
                .ToListAsync();

            return stories.Select(s => new StoryDTO(s)).ToList();
        }

        public async Task<StoryDTO> UpdateStoryAsync(int id, StoryDTO storyDto)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return null;
            }

            story.Title = storyDto.Title;
            story.Description = storyDto.Description;
            story.Department = storyDto.Department;

            await _context.SaveChangesAsync();

            return new StoryDTO(story);
        }

        public async Task<bool> DeleteStoryAsync(int id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return false;
            }

            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
