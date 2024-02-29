using MediatR;
using Stories.API.CQRS.Commands.StoryRequests;
using Stories.API.CQRS.Commands.StoryResponses;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.CQRS.Handlers.StoryHandler
{
    public class DeleteStoryHandler : IRequestHandler<DeleteStoryRequest, DeleteStoryResponse>
    {
        private readonly IStoryService _storyService;

        public DeleteStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public Task<DeleteStoryResponse> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
        {

            var storyDto = new StoryDTO
            {
                Id = request.Id,
            };

            var deleteStoryDto = await _storyService.CreateStoryAsync(storyDto);

            var result = new DeleteStoryResponse
            {
                Id = deleteStoryDto.Id,
            };

            return result;
        }
    }
}
