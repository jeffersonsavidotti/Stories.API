namespace Stories.API.CQRS.Commands.StoryResponses
{
    public class UpdateStoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
    }
}
