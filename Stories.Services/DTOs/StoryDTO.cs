using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class StoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int VotesCount { get; set; }

        public StoryDTO() { }
        // Construtor que mapeia uma instância de Story para StoryDTO
        public StoryDTO(Story story)
        {
            Id = story.Id;
            Title = story.Title;
            Description = story.Description;
            Department = story.Department;
            VotesCount = story.Votes?.Count ?? 0; // Contagem de votos, assumindo que Votes pode ser nulo
        }
    }
}

