using MediatR;

namespace Stories.API.CQRS.Commands.Story;

public class DeleteStoryCommand : IRequest<bool> // Retorna true se a operação for bem-sucedida
{
    public int Id { get; set; }
}

