using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class StoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }

        public StoryDTO() { }

        public StoryDTO(Story story)
        {
            Id = story.Id;
            Title = story.Title;
            Description = story.Description;
            Department = story.Department;
            PositiveVotesCount = story.Votes?.Count(v => v.VoteValue) ?? 0;
            NegativeVotesCount = story.Votes?.Count(v => !v.VoteValue) ?? 0;
        }
    }
}
