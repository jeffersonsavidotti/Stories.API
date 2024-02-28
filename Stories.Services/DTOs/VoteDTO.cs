using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class VoteDTO
    {
        //public Guid Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public bool VoteValue { get; set; }

        public VoteDTO() { }

        public VoteDTO(Vote vote)
        {
            IdStory = vote.IdStory;
            IdUser = vote.IdUser;
            VoteValue = vote.VoteValue;
        }
    }
}
