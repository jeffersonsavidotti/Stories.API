using MediatR;
using Stories.API.CQRS.Commands.StoryResponses;

namespace Stories.API.CQRS.Commands.StoryRequests;

public class UpdateStoryRequest : IRequest<UpdateStoryResponse>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
}
