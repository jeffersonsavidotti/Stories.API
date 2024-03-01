using MediatR;
using Stories.API.Applications.ViewModels;
using Stories.API.CQRS.Queries.Story;
using Stories.Services.Interfaces;

namespace Stories.API.CQRS.Handlers.StoryHandler
{
    public class GetAllStoriesHandler //: IRequestHandler<GetAllStoriesQuery, List<StoryViewModel>>
    {
        //private readonly IStoryService _storyService;

        //public GetAllStoriesHandler(IStoryService storyService)
        //{
        //    _storyService = storyService;
        //}

        //public async Task<List<StoryViewModel>> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
        //{
        //    var result = await _storyService.GetAllStoriesAsync();

        //    return result.Select(s => new StoryViewModel { Id = s.Id, Title = s.Title, Description = s.Description, Department = s.Department }).ToList();
        //}
    }
}
