using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Dtos;
using Kulba.Service.Bucket.Entities;
using Kulba.Service.Bucket.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Hosting;

namespace Kulba.Service.Bucket.Controllers
{
    [ApiController]
    [Route("bookmarks")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository bookmarkRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<BookmarkController> _logger;

        public BookmarkController(IBookmarkRepository bookmarkRepository, IMemoryCache memoryCache, ILogger<BookmarkController> logger)
        {
            this.bookmarkRepository = bookmarkRepository;
            this._logger = logger;
            this._memoryCache = memoryCache;

        }

        // Get /bookmarks
        [HttpGet]
        public async Task<IEnumerable<BookmarkDto>> GetBookmarks()
        {
            _logger.LogDebug("Hit GetBookmarksAsync service.");
            var bookmarks = (await bookmarkRepository.GetBookmarkItemsAsync())
                .Select(bookmarkItem => bookmarkItem.AsDto());
            return bookmarks;
        }


        // Get /bookmarks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkDto>> GetBookmarkItemById(Guid id)
        {
            _logger.LogDebug("Hit GetBookmarkItemById with Id: {id}.", id);
            var bookmark = await bookmarkRepository.GetBookmarkItemByIdAsync(id);
            if (bookmark is null)
            {
                return NotFound();  
            }
            return bookmark.AsDto();
        }

        // POST /bookmarks
        [HttpPost]
        public async Task<ActionResult<BookmarkDto>> PostBookmarkItem(CreateBookmarkDto bookmarkDto)
        {
            _logger.LogInformation("Create new Bookmark Item: {title}.", bookmarkDto.Title);
            BookmarkItem item = new()
            {
                Id = Guid.NewGuid(),
                Title = bookmarkDto.Title,
                Url = bookmarkDto.Url,
                CreatedDate = DateTimeOffset.UtcNow
            };            
            await bookmarkRepository.CreateBookmarkItemAsync(item);

            return CreatedAtAction(nameof(GetBookmarkItemById), new { id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBookmarkItem(Guid id, UpdateBookmarkDto bookmarkDto)
        {
            _logger.LogInformation("Update Bookmark Item: {title}.", bookmarkDto.Title);
            var existingBookmark = await bookmarkRepository.GetBookmarkItemByIdAsync(id);

            if (existingBookmark is null)
            {
                return NotFound();
            }

            BookmarkItem updatedBookmark = existingBookmark with {
                Title = bookmarkDto.Title,
                Url = bookmarkDto.Url
            };

            await bookmarkRepository.UpdateBookmarkItemAsync(updatedBookmark);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookmarkItem(Guid id)
        {
            _logger.LogInformation("Delete Bookmark Item Id: {id}.", id);
            var existingBookmark = await bookmarkRepository.GetBookmarkItemByIdAsync(id);

            if (existingBookmark is null)
            {
                return NotFound();
            }

           await bookmarkRepository.DeleteBookmarkItemAsync(id);

            return NoContent();

        }

    }

}