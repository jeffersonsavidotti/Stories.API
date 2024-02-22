using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                // Inicialização da lista de Votes é feita no modelo
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            // Retorna o DTO atualizado, incluindo a contagem de votos (se aplicável)
            return new StoryDTO(story);
        }

        public async Task<StoryDTO> GetStoryByIdAsync(int id)
        {
            var story = await _context.Stories
                .Include(s => s.Votes) // Garante que os votos sejam incluídos para calcular as contagens
                .FirstOrDefaultAsync(s => s.Id == id);

            return story != null ? new StoryDTO(story) : null;
        }

        public async Task<IEnumerable<StoryDTO>> GetAllStoriesAsync()
        {
            var stories = await _context.Stories
                .Include(s => s.Votes) // Inclui votos para cálculo das contagens
                .ToListAsync();

            return stories.Select(s => new StoryDTO(s)).ToList();
        }

        public async Task<StoryDTO> UpdateStoryAsync(int id, StoryDTO storyDto)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return null; // Ou considere lançar uma exceção
            }

            story.Title = storyDto.Title;
            story.Description = storyDto.Description;
            story.Department = storyDto.Department;
            // Contagens de votos são gerenciadas separadamente

            await _context.SaveChangesAsync();

            // Retorna o DTO atualizado com as contagens de votos
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
