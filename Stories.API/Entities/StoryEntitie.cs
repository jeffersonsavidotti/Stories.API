namespace Stories.API.Entities;

public class StoryEntitie
{
    public StoryEntitie(string title, string description, string departament)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Departament = departament;
    }
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Departament { get; private set;}
}
