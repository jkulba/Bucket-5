using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Entities;

namespace Kulba.Service.Bucket.Repositories
{
    public class InMemBookmarkRepository : IBookmarkRepository
    {
        private readonly List<BookmarkItem> bookmarkItems = new()
        {
            new BookmarkItem {Id = Guid.NewGuid(), Title = "Yahoo Home Page", Url = "https://www.yahoo.com/", CreatedDate = DateTimeOffset.UtcNow},
            new BookmarkItem {Id = Guid.NewGuid(), Title = "CNN Home Page", Url = "https://www.cnn.com/", CreatedDate = DateTimeOffset.UtcNow},
            new BookmarkItem {Id = Guid.NewGuid(), Title = "Washington Post", Url = "https://www.washingtonpost.com/", CreatedDate = DateTimeOffset.UtcNow},
            new BookmarkItem {Id = Guid.NewGuid(), Title = "Microsoft Home Page", Url = "https://www.microsoft.com/", CreatedDate = DateTimeOffset.UtcNow}
        };

        public async Task<BookmarkItem> GetBookmarkItemByIdAsync(Guid id)
        {
            var bookmarkItem = bookmarkItems.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(bookmarkItem);
        }

        public async Task<IEnumerable<BookmarkItem>> GetBookmarkItemsAsync()
        {
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