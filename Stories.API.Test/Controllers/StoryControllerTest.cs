using Moq;
using Stories.API.Controllers;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stories.API.CQRS.Commands.StoryRequests;
using Stories.API.CQRS.Commands.StoryResponses;

namespace Stories.API.Tests.Controllers
{
    public class StoryControllerTests
    {
        // GetAll
        [Fact]
        public async Task GetAllStories_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IStoryService>();
            mockService.Setup(service => service.GetAllStoriesAsync())
                       .ReturnsAsync(new List<StoryDTO>());

            var controller = new StoriesController(mockService.Object);

            // Act
            var result = await controller.GetAllStories();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //GetId
        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int storyId = 1;
            var mockService = new Mock<IStoryService>();
            mockService.Setup(service => service.GetStoryByIdAsync(storyId))
                       .ReturnsAsync(new StoryDTO());

            var controller = new StoriesController(mockService.Object);

            // Act
            var result = await controller.GetById(storyId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

      
    }
}
