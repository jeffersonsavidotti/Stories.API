//using MediatR;
//using Stories.Services.DTOs;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using Stories.API.CQRS.Queries.Story;

//namespace Stories.API.CQRS.Handlers.StoryHandler
//{
//    public class GetAllStoriesHandler : IRequestHandler<GetAllStoriesQuery, List<StoryDTO>>
//    {
//        private readonly AppDbContext _context;

//        public GetAllStoriesHandler(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<StoryDTO>> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
//        {
//            var stories = await _context.Stories
//                .Select(s => new StoryDTO
//                {
//                    Id = s.Id,
//                    Title = s.Title,
//                    Description = s.Description,
//                    Department = s.Department,
//                    PositiveVotesCount = s.Votes.Count(v => v.IsPositive),
//                    NegativeVotesCount = s.Votes.Count(v => !v.IsPositive)
//                })
//                .ToListAsync(cancellationToken);

//            return stories;
//        }
//    }
//}
