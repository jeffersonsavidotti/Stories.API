using MediatR;
using Stories.Services.DTOs;

namespace Stories.API.CQRS.Queries.Story;

// Define a query para obter todas as histórias
public class GetAllStoriesQuery : IRequest<IEnumerable<StoryDTO>>
{
}
