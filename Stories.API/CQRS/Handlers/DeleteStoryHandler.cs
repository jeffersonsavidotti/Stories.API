using MediatR;
using Microsoft.EntityFrameworkCore;
using Stories.API.CQRS.Commands.Story;
using Stories.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Stories.API.CQRS.Handlers.Story
{
    public class DeleteStoryHandler : IRequestHandler<DeleteStoryCommand, Unit>
    {
        private readonly IStoryService _storyService;

        public DeleteStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<Unit> Handle(DeleteStoryCommand request, CancellationToken cancellationToken)
        {
            var story = await _context.Stories.FindAsync(request.Id);

            if (story == null)
            {
                throw new NotFoundException("Story not found.");
            }

            _context.Stories.Remove(story);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
