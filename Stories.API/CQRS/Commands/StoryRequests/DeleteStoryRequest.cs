using MediatR;
using Stories.API.CQRS.Commands.StoryResponses;

namespace Stories.API.CQRS.Commands.StoryRequests;

public class DeleteStoryRequest : IRequest<CreateStoryResponse>
{
    public Guid Id { get; set; }
}

