using MediatR;
using Stories.Services.DTOs;
using Stories.API.CQRS.Queries.Story;
using Stories.Services.Interfaces;

namespace Stories.API.CQRS.Handlers.StoryHandler
{
    public class GetAllStoriesHandler : IRequestHandler<GetAllStoriesQuery, List<StoryDTO>>
    {
        private readonly IStoryService _storyService;

        public CreateStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<List<StoryDTO>> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
        {
            List<StoryDTO> stories = await _storyService.GetAllStoriesAsync();

            return stories;
        }
    }
}
