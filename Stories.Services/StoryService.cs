using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
                // Votes não são criados aqui; isso seria gerenciado separadamente.
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            storyDto.Id = story.Id; // Atualize o ID após a gravação no banco de dados.
            return storyDto;
        }

        public async Task<StoryDTO> GetStoryByIdAsync(int id)
        {
            var story = await _context.Stories
                .Where(s => s.Id == id)
                .Select(s => new StoryDTO(s)) // Supondo que você tenha um construtor adequado em StoryDTO
                .FirstOrDefaultAsync();

            return story;
        }

        public async Task<IEnumerable<StoryDTO>> GetAllStoriesAsync()
        {
            var stories = await _context.Stories
                .Select(s => new StoryDTO(s)) // Supondo que você tenha um construtor adequado em StoryDTO
                .ToListAsync();

            return stories;
        }

        public async Task<StoryDTO> UpdateStoryAsync(int id, StoryDTO storyDto)
        {
            var story = await _context.Stories.FirstOrDefaultAsync(s => s.Id == id);
            if (story == null)
            {
                return null; // Ou lance uma exceção adequada
            }

            story.Title = storyDto.Title;
            story.Description = storyDto.Description;
            story.Department = storyDto.Department;
            // Não atualize Votes aqui

            await _context.SaveChangesAsync();

            return storyDto; // Pode querer retornar uma nova consulta ao DTO para incluir quaisquer alterações derivadas do banco de dados
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
