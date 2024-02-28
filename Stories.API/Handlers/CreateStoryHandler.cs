using Microsoft.AspNetCore.Mvc;
using Stories.API.Applications.ViewModels;
using Stories.API.Commands.Requests;
using Stories.API.Commands.Responses;
using Stories.API.Entities;
using Stories.Services;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;

namespace Stories.API.Handlers;

public class CreateStoryHandler
{
    IStoryService _storyService;

    public CreateStoryHandler(IStoryService storyService)
    {
        _storyService = storyService;
    }

    //public CreateStoryResponse Hendle(CreateStoryRequest create)
    //{
    //    var story = new StoryEntitie(create.Title, create.Description, create.Departament);

    //    _storyService.CreateStoryAsync(story);



    //}
}