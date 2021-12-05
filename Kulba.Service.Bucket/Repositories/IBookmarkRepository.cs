using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Entities;

namespace Kulba.Service.Bucket.Repositories
{
    public interface IBookmarkRepository
    {
        Task<BookmarkItem> GetBookmarkItemAsync(Guid id);

        Task<IEnumerable<BookmarkItem>> GetBookmarkItemsAsync();

        Task CreateBookmarkAsync(BookmarkItem item);
    }
}