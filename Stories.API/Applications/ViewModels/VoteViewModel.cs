namespace Stories.API.Applications.ViewModels
{
    public class VoteViewModel
    {
        public Guid Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public bool VoteValue { get; set; }
    }
}
