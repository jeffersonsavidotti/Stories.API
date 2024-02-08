using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public Story Story { get; set; }
        public User User { get; set; }
        public bool VoteValue { get; set; }
    }
}
