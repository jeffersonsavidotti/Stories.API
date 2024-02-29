using MediatR;
using Microsoft.EntityFrameworkCore;
using Stories.API.CQRS.Commands.Story;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Stories.API.CQRS.Handlers.Story
{
    public class UpdateStoryHandler : IRequestHandler<UpdateStoryCommand, StoryDTO>
    {
        private readonly IStoryService _storyService;

        public UpdateStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<StoryDTO> Handle(UpdateStoryCommand request, CancellationToken cancellationToken)
        {
            var story = await _context.Stories.FindAsync(request.StoryDTO.Id);

            if (story == null)
            {
                throw new NotFoundException("Story not found.");
            }

            story.Title = request.StoryDTO.Title;
            story.Description = request.StoryDTO.Description;
            story.Department = request.StoryDTO.Department;
            // Atualizar outras propriedades conforme necessário

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
