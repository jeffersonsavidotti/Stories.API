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

        public List<StoryDTO> GetAll()
        {
            return _context.Stories.
                Select(s => new StoryDTO() { Id = s.Id, Title = s.Title, Description = s.Description, Department = s.Department }).
                ToList();
        }

        public StoryDTO GetById(int id)
        {
            return _context.Stories
                .Select(s => new StoryDTO() { Id = s.Id, Title = s.Title, Department = s.Department, Description = s.Description })
                .FirstOrDefault(f => f.Id == id);
        }

        public void AddStory(int id, StoryDTO story)
        {
            _context.Add( new StoryDTO() { Id = id, Title = story.Title, Description = story.Description, Department = story.Department });
            _context.SaveChanges();
        }

        public bool UpdateStory(int id, StoryDTO story)
        {
            var storyDTO = _context.Stories.SingleOrDefault(d => d.Id == id);
            
            if (storyDTO == null)
            {
                return false;
            }
            _context.Update(new StoryDTO() { Id = id, Title = story.Title, Description = story.Description, Department = story.Department });
            _context.SaveChanges();
            return true;
            
        }

        public bool DeleteStory(int id)
        {
            var story = _context.Stories.Find(id);
            if (story != null)
            {
                _context.Stories.Remove(story);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
