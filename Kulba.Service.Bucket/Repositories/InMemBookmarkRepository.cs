using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Entities;
using Microsoft.Extensions.Logging;

namespace Kulba.Service.Bucket.Repositories
{
    public class InMemBookmarkRepository : IBookmarkRepository
    {
        private readonly ILogger<InMemBookmarkRepository> _logger;

        private readonly List<BookmarkItem> bookmarkItems = new()
        {
            new BookmarkItem {Id = Guid.NewGuid(), Title = "Yahoo Home Page", Url = "https://www.yahoo.com/", CreatedDate = DateTimeOffset.UtcNow},
            new BookmarkItem {Id = Guid.NewGuid(), Title = "CNN Home Page", Url = "https://www.cnn.com/", CreatedDate = DateTimeOffset.UtcNow},
            new BookmarkItem {Id = Guid.NewGuid(), Title = "Washington Post", Url = "https://www.washingtonpost.com/", CreatedDate = DateTimeOffset.UtcNow},
            new BookmarkItem {Id = Guid.NewGuid(), Title = "Microsoft Home Page", Url = "https://www.microsoft.com/", CreatedDate = DateTimeOffset.UtcNow}
        };

        public InMemBookmarkRepository(ILogger<InMemBookmarkRepository> logger)
        {
            _logger = logger;
        }

        public async Task<BookmarkItem> GetBookmarkItemByIdAsync(Guid id)
        {
            var bookmarkItem = bookmarkItems.Where(item => item.Id == id).SingleOrDefault();
            _logger.LogInformation("GetBookmarkItemByIdAsync {bookmarkItem}", bookmarkItem);
            return await Task.FromResult(bookmarkItem);
        }

        public async Task<IEnumerable<BookmarkItem>> GetBookmarkItemsAsync()
        {
            _logger.LogInformation("GetBookmarkItemsAsync {bookmarkItems}", bookmarkItems);
            return await Task.FromResult(bookmarkItems);
        }

        public async Task CreateBookmarkItemAsync(BookmarkItem item)
        {
            bookmarkItems.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateBookmarkItemAsync(BookmarkItem item)
        {
            var index = bookmarkItems.FindIndex(existingBookmark => existingBookmark.Id == item.Id);
            bookmarkItems[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteBookmarkItemAsync(Guid id)
        {
            var index = bookmarkItems.FindIndex(existingBookmark => existingBookmark.Id == id);
            bookmarkItems.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}