using MediatR;
using Stories.API.CQRS.Commands.StoryResponses;

namespace Stories.API.CQRS.Commands.StoryRequests;

public class DeleteStoryRequest : IRequest<DeleteStoryResponse>
{
    public int Id { get; set; }
}

