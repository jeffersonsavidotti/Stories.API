using Stories.Infrastructure.Models;
using System.Linq; // Necessário para operações LINQ

namespace Stories.Services.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }

        public UserDTO() { }

        // Construtor que mapeia uma instância de User para UserDTO
        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            // Calcula a contagem de votos positivos e negativos
            PositiveVotesCount = user.Votes?.Count(v => v.VoteValue) ?? 0;
            NegativeVotesCount = user.Votes?.Count(v => !v.VoteValue) ?? 0;
        }
    }
}
