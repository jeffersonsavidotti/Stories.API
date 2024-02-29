using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}
