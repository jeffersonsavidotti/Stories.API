using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stories.Services
{
    public class VoteService : IVoteService
    {
        private readonly AppDbContext _context;

        public VoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VoteDTO> CreateVoteAsync(VoteDTO voteDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var vote = new Vote
                {
                    IdStory = voteDto.IdStory,
                    IdUser = voteDto.IdUser,
                    VoteValue = voteDto.VoteValue
                };

                _context.Votes.Add(vote);
                await _context.SaveChangesAsync();

                // Atualizar as contagens na Story
                var story = await _context.Stories.FindAsync(voteDto.IdStory);
                if (story != null)
                {
                    if (vote.VoteValue)
                    {
                        story.PositiveVotesCount++;
                    }
                    else
                    {
                        story.NegativeVotesCount++;
                    }
                    await _context.SaveChangesAsync();
                }

                // Opcional: Atualizar as contagens no User, se aplicável
                // var user = await _context.Users.FindAsync(voteDto.IdUser);
                // Atualize o usuário conforme necessário

                await transaction.CommitAsync();

                voteDto.Id = vote.Id; // Atualize o ID após salvar
                return voteDto;
            }
        }

        public async Task<IEnumerable<VoteDTO>> GetAllVotesAsync()
        {
            var votes = await _context.Votes
                .Select(v => new VoteDTO
                {
                    Id = v.Id,
                    IdStory = v.IdStory,
                    IdUser = v.IdUser,
                    VoteValue = v.VoteValue
                })
                .ToListAsync();

            return votes;
        }

        public async Task<VoteDTO> GetVoteByIdAsync(int id)
        {
            var vote = await _context.Votes
                .Where(v => v.Id == id)
                .Select(v => new VoteDTO
                {
                    Id = v.Id,
                    IdStory = v.IdStory,
                    IdUser = v.IdUser,
                    VoteValue = v.VoteValue
                })
                .FirstOrDefaultAsync();

            return vote;
        }

        public async Task<bool> DeleteVoteAsync(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return false;
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                _context.Votes.Remove(vote);

                // Atualizar as contagens na Story
                var story = await _context.Stories.FindAsync(vote.IdStory);
                if (story != null)
                {
                    if (vote.VoteValue)
                    {
                        story.PositiveVotesCount--;
                    }
                    else
                    {
                        story.NegativeVotesCount--;
                    }
                    await _context.SaveChangesAsync();
                }

                // Opcional: Atualizar as contagens no User, se aplicável
                // var user = await _context.Users.FindAsync(vote.IdUser);
                // Atualize o usuário conforme necessário

                await transaction.CommitAsync();
                return true;
            }
        }
    }
}
