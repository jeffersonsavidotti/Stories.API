using MediatR;
using Stories.Services.DTOs;

namespace Stories.API.CQRS.Queries;

public class GetStoryByIdQuery : IRequest<StoryDTO> // Retorna uma única história
{
    public int Id { get; set; }

    public GetStoryByIdQuery(int id)
    {
        Id = id;
    }
}
