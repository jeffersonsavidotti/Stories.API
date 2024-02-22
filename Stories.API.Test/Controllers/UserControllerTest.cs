using Moq;
using Stories.API.Controllers;
using Stories.API.Applications.ViewModels;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Stories.API.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetAllUsersAsync())
                       .ReturnsAsync(new List<UserDTO>());

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.GetAllUsers();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetUserByIdAsync(userId))
                       .ReturnsAsync(new UserDTO());

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.GetById(userId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_WithValidModel_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.CreateUserAsync(It.IsAny<UserDTO>()))
                       .ReturnsAsync(new UserDTO());

            var controller = new UserController(mockService.Object);
            var viewModel = new UserViewModel { Name = "Test User" };

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Edit_WithValidModel_ReturnsNoContentResult()
        {
            // Arrange
            int userId = 1;
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.UpdateUserAsync(userId, It.IsAny<UserDTO>()))
                       .ReturnsAsync(new UserDTO());

            var controller = new UserController(mockService.Object);
            var viewModel = new UserViewModel { Id = userId, Name = "Updated Test User" };

            // Act
            var result = await controller.Edit(userId, viewModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            int userId = 1;
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.DeleteUserAsync(userId))
                       .ReturnsAsync(true);

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
