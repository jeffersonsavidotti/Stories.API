using Stories.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Infrastructure.Repository
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Story>> GetAllAsync();
        Task<Story> GetByIdAsync(int id);
        Task AddStoryAsync(Story story);
        Task<bool> UpdateStoryAsync(Story story);
        Task<bool> DeleteStoryAsync(int id);
    }
}
