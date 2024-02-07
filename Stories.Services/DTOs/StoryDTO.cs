using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class StoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }
        public int Like {  get; set; }
        public int Deslike { get; set; }
    }
}
