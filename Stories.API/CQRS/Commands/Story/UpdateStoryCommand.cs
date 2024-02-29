using MediatR;

namespace Stories.API.CQRS.Commands.Story;

public class UpdateStoryCommand : IRequest<bool> // Retorna true se a operação for bem-sucedida
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
}
