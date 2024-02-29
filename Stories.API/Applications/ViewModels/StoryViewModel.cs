using Stories.Infrastructure.Models;

namespace Stories.API.Applications.ViewModels
{
    public class StoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }
    }
}
