//using MediatR;
//using Stories.API.CQRS.Commands.StoryRequests;
//using Stories.API.CQRS.Commands.StoryResponses;
//using Stories.Services.Interfaces;

//namespace Stories.API.CQRS.Handlers.StoryHandler
//{
//    public class DeleteStoryHandler : IRequestHandler<DeleteStoryRequest, DeleteStoryResponse>
//    {
//        private readonly IStoryService _storyService;

//        public DeleteStoryHandler(IStoryService storyService)
//        {
//            _storyService = storyService;
//        }

//        public Task<DeleteStoryResponse> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
//        {
//            //var story = await _context.Stories.FindAsync(request.Id);

//            //if (story == null)
//            //{
//            //    throw new NotFoundException("Story not found.");
//            //}

//            //_context.Stories.Remove(story);
//            //await _context.SaveChangesAsync(cancellationToken);

//            //return Unit.Value;
//        }
//    }
//}
