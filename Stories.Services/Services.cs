using Stories.Infrastructure.Models;
using Stories.Infrastructure.Repository;
using Stories.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stories.Services.Service;
using static Stories.Services.Services;

namespace Stories.Services
{
        public class Service : IService
        {
            private readonly IStoryRepository _storyRepository;
            private readonly IVoteRepository _voteRepository;

            public Service(IStoryRepository storyRepository, IVoteRepository voteRepository)
            {
                _storyRepository = storyRepository;
                _voteRepository = voteRepository;
            }

            public async Task<IEnumerable<StoryDTO>> GetAllStoriesAsync()
            {
                var stories = await _storyRepository.GetAllAsync();

                // Convertendo entidades para modelos DTO
                var storyModels = stories.Select(s => new StoryDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Department = s.Department,
                    Likes = s.Votes.Count(v => v.IsLike),
                    Dislikes = s.Votes.Count(v => !v.IsLike)
                });

                return storyModels;
            }

            public async Task<bool> VoteStoryAsync(int storyId, int userId, bool isLike)
            {
                // Verificar se o voto já existe
                var existingVote = await _voteRepository.GetVoteByStoryAndUserAsync(storyId, userId);

                if (existingVote != null)
                {
                    // Atualizar o voto existente se diferente
                    if (existingVote.IsLike != isLike)
                    {
                        existingVote.IsLike = isLike;
                        return await _voteRepository.UpdateVoteAsync(existingVote);
                    }
                    // Se o voto for o mesmo, não faça nada
                    return true; // Ou considere retornar false ou lançar uma exceção conforme a necessidade
                }
                else
                {
                    // Criar um novo voto
                    var newVote = new Vote
                    {
                        StoryId = storyId,
                        UserId = userId,
                        IsLike = isLike
                    };
                    return await _voteRepository.AddVoteAsync(newVote);
                }
            }

        // StoryService.cs em Story.Services
        public class StoryService : IStoryService
        {
            private readonly IStoryRepository _storyRepository;

            public StoryService(IStoryRepository storyRepository)
            {
                _storyRepository = storyRepository;
            }

            public async Task<IEnumerable<Story>> GetAllStoriesAsync()
            {
                return await _storyRepository.GetAllAsync();
            }

            public async Task<bool> VoteStoryAsync(int storyId, int userId, bool isLike)
            {
                var vote = new Vote
                {
                    StoryId = storyId,
                    UserId = userId,
                    IsLike = isLike
                };
                return await _storyRepository.SaveVoteAsync(vote);
            }

            public class StoryService : IStoryService
            {
                private readonly IStoryRepository _storyRepository; // Supondo a existência deste repositório

                public StoryService(IStoryRepository storyRepository)
                {
                    _storyRepository = storyRepository;
                }

                public async Task<IEnumerable<StoryDTO>> GetAllStoriesAsync()
                {
                    var stories = await _storyRepository.GetAllAsync();
                    return stories.Select(s => new StoryDTO
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        // Assumindo que há um mapeamento de Story para StoryDTO
                    }).ToList();
                }

                public async Task<StoryDTO> GetStoryByIdAsync(int id)
                {
                    var story = await _storyRepository.GetByIdAsync(id);
                    if (story == null) return null;
                    return new StoryDTO
                    {
                        Id = story.Id,
                        Title = story.Title,
                        Description = story.Description,
                        // Mapeamento adicional
                    };
                }

                public async Task<StoryDTO> AddStoryAsync(StoryDTO storyDto)
                {
                    // Implementação da adição de uma nova história
                    // Converta StoryDTO para a entidade Story e chame _storyRepository.AddAsync()
                }

                public async Task<bool> UpdateStoryAsync(int id, StoryDTO storyDto)
                {
                    // Implementação da atualização de uma história
                    // Verifique se a história existe e atualize-a
                }

                public async Task<bool> DeleteStoryAsync(int id)
                {
                    // Implementação da exclusão de uma história
                    // Chame _storyRepository.DeleteAsync(id)
                }

            }

    }
}
