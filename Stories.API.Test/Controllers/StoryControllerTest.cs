using Microsoft.AspNetCore.Mvc;
using Moq;
using Stories.API.Controllers;
using Stories.Services;
using Stories.Services.DTOs;
using Stories.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Test.Controllers
{
    public  class StoryControllerTest
    {
        public Mock<IStoryService> StoryService;
        public StoryController StoryController; 

        public StoryControllerTest()
        {
            StoryService = new Mock<IStoryService>();
            StoryController = new StoryController(StoryService.Object);
        }

        [Fact]
        public async Task GetAll_HesData_Ok()
        {
            var data = new List<StoryDTO>() 
            {
                new StoryDTO()
            };

            StoryService.Setup(x => x.GetAllStoriesAsync()).ReturnsAsync(data);

            var result = await StoryController.GetAllStories();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetId_HesData_Ok()
        {

            StoryService.Setup(x=> x.GetStoryByIdAsync(It.IsAny<int>())).ReturnsAsync(new StoryDTO());

            var result = await StoryController.GetById(1);

            Assert.IsType<OkObjectResult>(result);
        }

        
    }
}
