using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Tests
{
    public class StoryServiceTests
    {
        [Fact]
        public async Task CreateStoryAsync_ReturnsStoryDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateStoryAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var service = new StoryService(context);
                var storyDto = new StoryDTO { Title = "Test Story", Description = "Test Description", Department = "Test Department" };

                // Act
                var createdStoryDto = await service.CreateStoryAsync(storyDto);

                // Assert
                Assert.NotNull(createdStoryDto);
                Assert.Equal(storyDto.Title, createdStoryDto.Title);
                Assert.Equal(storyDto.Description, createdStoryDto.Description);
                Assert.Equal(storyDto.Department, createdStoryDto.Department);
                Assert.True(createdStoryDto.Id > 0);
            }
        }

        [Fact]
        public async Task GetStoryByIdAsync_WithValidId_ReturnsStoryDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetStoryByIdAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Seed the database with a story
                var story = new Story { Title = "Test Story", Description = "Test Description", Department = "Test Department" };
                context.Stories.Add(story);
                context.SaveChanges();

                var service = new StoryService(context);

                // Act
                var storyDto = await service.GetStoryByIdAsync(story.Id);

                // Assert
                Assert.NotNull(storyDto);
                Assert.Equal(story.Id, storyDto.Id);
                Assert.Equal(story.Title, storyDto.Title);
                Assert.Equal(story.Description, storyDto.Description);
                Assert.Equal(story.Department, storyDto.Department);
            }
        }

        [Fact]
        public async Task GetAllStoriesAsync_ReturnsListOfStoryDtos()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllStoriesAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Seed the database with stories
                var stories = new List<Story>
                {
                    new Story { Title = "Story 1", Description = "Description 1", Department = "Department 1" },
                    new Story { Title = "Story 2", Description = "Description 2", Department = "Department 2" },
                    new Story { Title = "Story 3", Description = "Description 3", Department = "Department 3" }
                };
                context.Stories.AddRange(stories);
                context.SaveChanges();

                var service = new StoryService(context);

                // Act
                var allStories = await service.GetAllStoriesAsync();

                // Assert
                Assert.NotNull(allStories);
                Assert.Equal(stories.Count, allStories.Count());
                foreach (var story in stories)
                {
                    Assert.Contains(allStories, s => s.Id == story.Id && s.Title == story.Title && s.Description == story.Description && s.Department == story.Department);
                }
            }
        }

        [Fact]
        public async Task UpdateStoryAsync_WithValidId_ReturnsUpdatedStoryDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateStoryAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Seed the database with a story
                var story = new Story { Title = "Test Story", Description = "Test Description", Department = "Test Department" };
                context.Stories.Add(story);
                context.SaveChanges();

                var service = new StoryService(context);
                var updatedStoryDto = new StoryDTO { Id = story.Id, Title = "Updated Title", Description = "Updated Description", Department = "Updated Department" };

                // Act
                var result = await service.UpdateStoryAsync(story.Id, updatedStoryDto);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(updatedStoryDto.Id, result.Id);
                Assert.Equal(updatedStoryDto.Title, result.Title);
                Assert.Equal(updatedStoryDto.Description, result.Description);
                Assert.Equal(updatedStoryDto.Department, result.Department);
            }
        }

        [Fact]
        public async Task DeleteStoryAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteStoryAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var story = new Story { Title = "Test Story", Description = "Test Description", Department = "Test Department" };
                context.Stories.Add(story);
                context.SaveChanges();

                var service = new StoryService(context);

                // Act
                var result = await service.DeleteStoryAsync(story.Id);

                // Assert
                Assert.True(result);
                Assert.Null(context.Stories.Find(story.Id));
            }
        }
    }
}
