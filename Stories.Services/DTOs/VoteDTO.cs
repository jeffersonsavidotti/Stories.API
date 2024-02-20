using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public bool VoteValue { get; set; } // Representa se o voto é positivo (true) ou negativo (false)

        public VoteDTO() { }

        // Construtor que mapeia uma instância de Vote para VoteDTO
        public VoteDTO(Vote vote)
        {
            Id = vote.Id;
            IdStory = vote.IdStory;
            IdUser = vote.IdUser;
            VoteValue = vote.VoteValue; // Captura o valor do voto (positivo/negativo)
        }
    }
}
