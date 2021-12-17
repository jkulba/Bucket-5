using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kulba.Service.Bucket.Controllers;
using Kulba.Service.Bucket.Entities;
using Kulba.Service.Bucket.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Kulba.Service.Bucket.Tests
{
    public class BookmarkControllerTests
    {
        private readonly Mock<IBookmarkRepository> bookmarkRepositoryStub = new();
        private readonly Mock<ILogger<BookmarkController>> loggerStub = new();
        private readonly Random rand = new();

         [Fact]
        public async Task GetBookmarkItemAsync_WithNoExistingBookmark_ReturnsNotFound()
        {
            // Arrange
            bookmarkRepositoryStub.Setup(repo => repo.GetBookmarkItemByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((BookmarkItem)null);
            var controller = new BookmarkController(bookmarkRepositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetBookmarkItemById(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        
    }
}