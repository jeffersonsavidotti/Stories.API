using MediatR;
using Stories.API.CQRS.Commands.StoryRequests;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using Stories.Infrastructure.Models;
using Stories.API.CQRS.Commands.StoryResponses;
using Stories.Services;
using Stories.API.Applications.ViewModels;

namespace Stories.API.CQRS.Handlers.StoryHandler 
{
    public class CreateStoryHandler : IRequestHandler<CreateStoryRequest, CreateStoryResponse>
    {
        private readonly IStoryService _storyService;

        public CreateStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }
        public async Task<CreateStoryResponse> Handle(CreateStoryRequest request, CancellationToken cancellationToken)
        {
            var storyView = new StoryViewModel
            {
                Title = request.Title,
                Description = request.Description,
                Department = request.Department,
            };

            var storyDto = new StoryDTO
            {
                Title = storyView.Title,
                Description = storyView.Description,
                Department = storyView.Department,
            };

            var createdStoryDto = await  _storyService.CreateStoryAsync(storyDto);
            
            var result = new CreateStoryResponse
            {
                Id = createdStoryDto.Id,
                Title = createdStoryDto.Title,
                Description = createdStoryDto.Description,
                Department = createdStoryDto.Department,
            };

            return result;
        }
    }
}
