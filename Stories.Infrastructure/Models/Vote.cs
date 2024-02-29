namespace Stories.Infrastructure.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public bool VoteValue { get; set; }
    }
}
