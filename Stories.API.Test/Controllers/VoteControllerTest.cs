using Moq;
using Stories.API.Controllers;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Stories.API.Tests.Controllers
{
    public class VoteControllerTests
    {
        //Get all
        [Fact]
        public async Task GetAllVotes_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IVoteService>();
            mockService.Setup(service => service.GetAllVotesAsync())
                       .ReturnsAsync(new List<VoteDTO>());

            var controller = new VoteController(mockService.Object);

            // Act
            var result = await controller.GetAllVotes();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //Add
        [Fact]
        public async Task Create_WithValidModel_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockService = new Mock<IVoteService>();
            mockService.Setup(service => service.CreateVoteAsync(It.IsAny<VoteDTO>()))
                       .ReturnsAsync(new VoteDTO());

            var controller = new VoteController(mockService.Object);
            var viewModel = new VoteViewModel { IdStory = 1, IdUser = 1, VoteValue = true };

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
