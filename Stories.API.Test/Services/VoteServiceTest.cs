﻿using Microsoft.EntityFrameworkCore;
using Stories.Infrastructure.Models;
using Stories.Services.DTOs;

namespace Stories.Services.Tests
{
    public class VoteServiceTests
    {
        [Fact]
        public async Task CreateVoteAsync_ReturnsVoteDtoWithId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateVoteAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var service = new VoteService(context);
                var voteDto = new VoteDTO { IdStory = 1, IdUser = 1, VoteValue = true };

                // Act
                var createdVoteDto = await service.CreateVoteAsync(voteDto);

                // Assert
                Assert.NotNull(createdVoteDto);
                Assert.True(createdVoteDto.Id > 0);
            }
        }

        [Fact]
        public async Task GetAllVotesAsync_ReturnsListOfVoteDtos()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllVotesAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var votes = new List<Vote>
                {
                    new Vote { IdStory = 1, IdUser = 1, VoteValue = true },
                    new Vote { IdStory = 2, IdUser = 2, VoteValue = false },
                    new Vote { IdStory = 3, IdUser = 3, VoteValue = true }
                };
                context.Votes.AddRange(votes);
                context.SaveChanges();

                var service = new VoteService(context);

                // Act
                var allVotes = await service.GetAllVotesAsync();

                // Assert
                Assert.NotNull(allVotes);
                Assert.Equal(votes.Count, allVotes.Count());
            }
        }

        [Fact]
        public async Task GetVoteByIdAsync_WithValidId_ReturnsVoteDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GetVoteByIdAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var vote = new Vote { IdStory = 1, IdUser = 1, VoteValue = true };
                context.Votes.Add(vote);
                context.SaveChanges();

                var service = new VoteService(context);

                // Act
                var voteDto = await service.GetVoteByIdAsync(vote.Id);

                // Assert
                Assert.NotNull(voteDto);
                Assert.Equal(vote.Id, voteDto.Id);
            }
        }

        [Fact]
        public async Task DeleteVoteAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteVoteAsync_Database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var vote = new Vote { IdStory = 1, IdUser = 1, VoteValue = true };
                context.Votes.Add(vote);
                context.SaveChanges();

                var service = new VoteService(context);

                // Act
                var result = await service.DeleteVoteAsync(vote.Id);

                // Assert
                Assert.True(result);
                Assert.Null(context.Votes.Find(vote.Id));
            }
        }
    }
}
