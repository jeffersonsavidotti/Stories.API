using MediatR;

namespace Stories.API.CQRS.Commands.Story;

public class CreateStoryCommand : IRequest<int> // Retorna o ID da história criada
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
}
