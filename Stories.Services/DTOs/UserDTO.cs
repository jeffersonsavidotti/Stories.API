using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            PositiveVotesCount = user.Votes?.Count(v => v.VoteValue) ?? 0;
            NegativeVotesCount = user.Votes?.Count(v => !v.VoteValue) ?? 0;
        }
    }
}
