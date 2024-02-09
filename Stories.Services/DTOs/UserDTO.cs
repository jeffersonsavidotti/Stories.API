using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
