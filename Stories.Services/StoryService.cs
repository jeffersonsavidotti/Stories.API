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

        public async Task<IEnumerable<StoryDTO>> GetAllAsync()
        {
            return _context.Stories.
                Select(s => new StoryDTO() { Id = s.Id, Title = s.Title, Description = s.Description, Department = s.Department }).
                ToList();
        }
        public async Task<StoryDTO> GetByIdAsync(int id)
        {

            return _context.Stories
                .Select(s => new StoryDTO() { Id = s.Id, Title = s.Title, Department = s.Department, Description = s.Description })
                .FirstOrDefault(f => f.Id == id);
        }
        public async Task AddStoryAsync(StoryDTO storyDTO) //<StoryDTO>
        {
            var story = new Story
            {
                Title = storyDTO.Title,
                Description = storyDTO.Description,
                Department = storyDTO.Department
            };
            await _context.AddAsync(story);
            await _context.SaveChangesAsync();
            //storyDTO.Id = story.Id;
            //return storyDTO;
        }
        public async Task UpdateStoryAsync(int id, StoryDTO storyDTO)
        {
            var data = _context.Stories.FirstOrDefault(d => d.Id == id);
            if (data == null)
            {
                Console.WriteLine("Id não encontrado");
            }
            Story story = new Story()
            {
                Id = id,
                Title = storyDTO.Title,
                Description = storyDTO.Description,
                Department = storyDTO.Department
            };
            _context.Update(storyDTO);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteStoryAsync(int id)
        {
            // Verifica se a história existe
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return false;
            }

            // Chamada ao repositório para remover a história
            _context.Remove(story);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        #region - Teste de metodos com DTO
        //public async Task<IEnumerable<StoryDTO>> GetAllAsync()
        //{
        //    return _context.Stories.
        //        Select(s => new StoryDTO() { Id = s.Id, Title = s.Title, Description = s.Description, Department = s.Department }).
        //        ToList();
        //}

        //public async Task<StoryDTO> GetByIdAsync(int id)
        //{

        //    return _context.Stories
        //        .Select(s => new StoryDTO() { Id = s.Id, Title = s.Title, Department = s.Department, Description = s.Description })
        //        .FirstOrDefault(f => f.Id == id);
        //}

        //public async Task AddStoryAsync(StoryDTO storyDTO)
        //{
        //    Story story = new Story()
        //    {
        //        Title = storyDTO.Title,
        //        Description = storyDTO.Description,
        //        Department = storyDTO.Department
        //    };
        //    await _context.AddAsync(storyDTO);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateStoryAsync(int id, StoryDTO storyDTO)
        //{
        //    var data = _context.Stories.FirstOrDefault(d => d.Id == id);

        //    if (data == null)
        //    {
        //        Console.WriteLine("Id não encontrado");
        //    }
        //    Story story = new Story()
        //    {
        //        Id = id,
        //        Title = storyDTO.Title,
        //        Description = storyDTO.Description,
        //        Department = storyDTO.Department
        //    };
        //    _context.Update(storyDTO);
        //    await _context.SaveChangesAsync();

        //}

        //public async Task<bool> DeleteStory(int id)
        //{
        //    await _context.Stories.Remove(id);
        //}
        #endregion
    }
}
