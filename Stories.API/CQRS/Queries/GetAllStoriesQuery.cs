using MediatR;
using Stories.Services.DTOs;

namespace Stories.API.CQRS.Queries.Story;

public class GetAllStoriesQuery : IRequest<List<StoryDTO>>
{
}
