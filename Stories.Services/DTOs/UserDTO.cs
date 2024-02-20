using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VotesCount { get; set; } // Representa a quantidade de votos

        public UserDTO() { }
        // Construtor que mapeia uma instância de User para UserDTO
        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            VotesCount = user.Votes?.Count ?? 0; // Contagem de votos, assumindo que Votes pode ser nulo
        }
    }
}
