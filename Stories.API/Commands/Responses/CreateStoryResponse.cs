namespace Stories.API.Commands.Responses;

public class CreateStoryResponse
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Departament { get; private set; }
}
