using MediatR;
using Stories.API.Commands.Responses;

namespace Stories.API.Commands.Requests;

public class CreateStoryRequest : IRequest<CreateStoryResponse>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Departament { get; private set; }
}
