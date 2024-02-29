using MediatR;
using Stories.API.CQRS.Commands.Story;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using Stories.Infrastructure.Models;

namespace Stories.API.CQRS.Handlers.Story
{
    public class CreateStoryHandler : IRequestHandler<CreateStoryCommand, StoryDTO>
    {
        private readonly IStoryService _storyService;

        public CreateStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<StoryDTO> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
        {
            var story = new S
            {
                Title = request.StoryDTO.Title,
                Description = request.StoryDTO.Description,
                Department = request.StoryDTO.Department,
                // Atribuir outras propriedades conforme necessário
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync(cancellationToken);

            return new StoryDTO
            {
                Id = story.Id,
                Title = story.Title,
                Description = story.Description,
                Department = story.Department,
                // Copiar outras propriedades para o DTO
            };
        }

    }
}
