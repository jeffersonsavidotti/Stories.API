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

        ////Add
        //[Fact]
        //public async Task Create_WithValidModel_ReturnsCreatedAtActionResult()
        //{
        //    // Arrange
        //    var mockService = new Mock<IStoryService>();
        //    mockService.Setup(service => service.CreateStoryAsync(It.IsAny<StoryDTO>()))
        //               .ReturnsAsync(new StoryDTO());

        //    var controller = new StoryController(mockService.Object);
        //    var request = new CreateStoryResponse { Title = "Projeto API", Description = "Desenvolver em 15 dias", Department = "Dev" };

        //    // Act
        //    var result = await controller.Create(request);

        //    // Assert
        //    Assert.IsType<CreatedAtActionResult>(result);
        //}


        // Update
        [Fact]
        public async Task Edit_WithValidModel_ReturnsNoContentResult()
        {
            // Arrange
            int storyId = 1;
            var mockService = new Mock<IStoryService>();
            mockService.Setup(service => service.UpdateStoryAsync(storyId, It.IsAny<StoryDTO>()))
                       .ReturnsAsync(new StoryDTO());

            var controller = new StoriesController(mockService.Object);
            var viewModel = new StoryViewModel { Id = storyId, Title = "Projeto API", Description = "Desenvolver em 15 dias", Department = "Dev" };

            // Act
            var result = await controller.Edit(storyId, viewModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        //Delete
        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            int storyId = 1;
            var mockService = new Mock<IStoryService>();
            mockService.Setup(service => service.DeleteStoryAsync(storyId))
                       .ReturnsAsync(true);

            var controller = new StoriesController(mockService.Object);

            // Act
            var result = await controller.Delete(storyId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
