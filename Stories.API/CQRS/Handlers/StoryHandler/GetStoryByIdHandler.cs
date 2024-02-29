//using MediatR;
//using Stories.Services.DTOs;
//using Microsoft.EntityFrameworkCore;
//using Stories.API.CQRS.Queries;

//namespace Stories.API.CQRS.Handlers.StoryHandler
//{
//    public class GetStoryByIdHandler : IRequestHandler<GetStoryByIdQuery, StoryDTO>
//    {
//        private readonly AppDbContext _context;

//        public GetStoryByIdHandler(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<StoryDTO> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
//        {
//            var story = await _context.Stories
//                .Where(s => s.Id == request.Id)
//                .Select(s => new StoryDTO
//                {
//                    Id = s.Id,
//                    Title = s.Title,
//                    Description = s.Description,
//                    Department = s.Department,
//                    PositiveVotesCount = s.Votes.Count(v => v.IsPositive),
//                    NegativeVotesCount = s.Votes.Count(v => !v.IsPositive)
//                })
//                .FirstOrDefaultAsync(cancellationToken);

//            if (story == null)
//            {
//                throw new NotFoundException("Story not found.");
//            }

//            return story;
//        }
//    }
//}
