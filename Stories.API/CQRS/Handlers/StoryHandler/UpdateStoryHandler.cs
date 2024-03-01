using MediatR;
using Stories.API.Applications.ViewModels;
using Stories.API.CQRS.Commands.StoryRequests;
using Stories.API.CQRS.Commands.StoryResponses;
using Stories.Services.Interfaces;

namespace Stories.API.CQRS.Handlers.StoryHandler
{
    public class UpdateStoryHandler : IRequestHandler<UpdateStoryRequest, UpdateStoryResponse>
    {
        private readonly IStoryService _storyService;

        public UpdateStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<UpdateStoryResponse> Handle(UpdateStoryRequest request, CancellationToken cancellationToken)
        {
            var getStory = await _storyService.GetStoryByIdAsync(request.Id);

            if (getStory is null)
            {
                throw new InvalidOperationException("Story not found");
            }
            var storyView = new StoryViewModel
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Department = request.Department
            };

            getStory.Id = request.Id;
            getStory.Title = request.Title;
            getStory.Description = request.Description;
            getStory.Department = request.Department;
            int id = request.Id;

            var story = await _storyService.UpdateStoryAsync(id, getStory);

            var result = new UpdateStoryResponse
            {
                Id = story.Id,
                Title = story.Title,
                Description = story.Description,
                Department = story.Department
            };

            return result;
        }

    }
}
