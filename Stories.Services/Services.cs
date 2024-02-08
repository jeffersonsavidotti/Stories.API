using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Stories.Infrastructure.Models;
using Stories.Infrastructure.Repository;
using Stories.Services.DTOs;

namespace Stories.Services;

    public class Services : IServices
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IVoteRepository _voteRepository;

        public Services(IStoryRepository storyRepository, IVoteRepository voteRepository)
        {
            _storyRepository = storyRepository;
            _voteRepository = voteRepository;
        }

        public async Task<StoryDTO> AddStoryAsync(StoryDTO storyDto)
        {
            var story = new Story
            {
                Title = storyDto.Title,
                Description = storyDto.Description,
                Department = storyDto.Department
            };
            await _storyRepository.AddStoryAsync(story);

            storyDto.Id = story.Id;
            return storyDto;
        }

        public async Task<IEnumerable<StoryDTO>> GetAllStoriesAsync()
        {
            var stories = await _storyRepository.GetAllAsync();

            var storyModels = stories.Select(s => new StoryDTO
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Department = s.Department,
                Likes = s.Votes.Count(v => v.VoteValue == true),
                Dislikes = s.Votes.Count(v => !v.VoteValue == true)
            });

            return storyModels;
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
                Department = story.Department
            };
        }

        //public async Task<bool> VoteStoryAsync(int storyId, int userId, bool isLike)
        //{
        //    VoteDTO voteDTO = new VoteDTO
        //    {
        //        IdStory = storyId,
        //        IdUser = userId,
        //        VoteValue = isLike
        //    };
        //    return await _voteRepository.AddVoteAsync(voteDTO);
        //}

    //Task<StoryDTO> IServices.DeleteStoryAsync(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<bool> IServices.UpdateStoryAsync(int id, StoryDTO storyDto)
    //{
    //    throw new NotImplementedException();
    //}
}
