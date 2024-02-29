using MediatR;
using Stories.Services.DTOs;

namespace Stories.API.CQRS.Queries;

public class GetStoryByIdQuery : IRequest<StoryDTO>
{
    public int Id { get; set; }

    public GetStoryByIdQuery(int id)
    {
        Id = id;
    }
}
