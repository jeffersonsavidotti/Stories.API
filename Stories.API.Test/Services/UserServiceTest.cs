using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_ReturnsUserDtoWithId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateUserAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var service = new UserService(context);
                var userDto = new UserDTO { Name = "Test User" };

                // Act
                var createdUserDto = await service.CreateUserAsync(userDto);

                // Assert
                Assert.NotNull(createdUserDto);
                Assert.Equal(userDto.Name, createdUserDto.Name);
                Assert.True(createdUserDto.Id > 0);
            }
        }

        [Fact]
        public async Task GetUserByIdAsync_WithValidId_ReturnsUserDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetUserByIdAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User { Name = "Test User" };
                context.Users.Add(user);
                context.SaveChanges();

                var service = new UserService(context);

                // Act
                var userDto = await service.GetUserByIdAsync(user.Id);

                // Assert
                Assert.NotNull(userDto);
                Assert.Equal(user.Id, userDto.Id);
                Assert.Equal(user.Name, userDto.Name);
            }
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsListOfUserDtos()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllUsersAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var users = new List<User>
                {
                    new User { Name = "User 1" },
                    new User { Name = "User 2" },
                    new User { Name = "User 3" }
                };
                context.Users.AddRange(users);
                context.SaveChanges();

                var service = new UserService(context);

                // Act
                var allUsers = await service.GetAllUsersAsync();

                // Assert
                Assert.NotNull(allUsers);
                Assert.Equal(users.Count, allUsers.Count());
                foreach (var user in users)
                {
                    Assert.Contains(allUsers, u => u.Id == user.Id && u.Name == user.Name);
                }
            }
        }

        [Fact]
        public async Task UpdateUserAsync_WithValidId_ReturnsUpdatedUserDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateUserAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User { Name = "Test User" };
                context.Users.Add(user);
                context.SaveChanges();

                var service = new UserService(context);
                var updatedUserDto = new UserDTO { Id = user.Id, Name = "Updated User" };

                // Act
                var result = await service.UpdateUserAsync(user.Id, updatedUserDto);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(updatedUserDto.Id, result.Id);
                Assert.Equal(updatedUserDto.Name, result.Name);
            }
        }

        [Fact]
        public async Task DeleteUserAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User { Name = "Test User" };
                context.Users.Add(user);
                context.SaveChanges();

                var service = new UserService(context);

                // Act
                var result = await service.DeleteUserAsync(user.Id);

                // Assert
                Assert.True(result);
                Assert.Null(context.Users.Find(user.Id));
            }
        }
    }
}
