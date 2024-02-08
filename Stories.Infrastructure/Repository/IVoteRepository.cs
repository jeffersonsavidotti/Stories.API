using Stories.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Infrastructure.Repository
{
    public interface IVoteRepository
    {
        Task<Vote> GetVoteByStoryAndUserAsync(int storyId, int userId);
        Task<bool> AddVoteAsync(Vote vote);
        Task<bool> UpdateVoteAsync(Vote vote);
    }
}
