using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Dtos;
using Kulba.Service.Bucket.Entities;
using Kulba.Service.Bucket.Extensions;
using Kulba.Service.Bucket.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Kulba.Service.Bucket.Controllers
{
    [ApiController]
    [Route("bookmarks")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository bookmarkRepository;
        private readonly ILogger<BookmarkController> logger;

        public BookmarkController(IBookmarkRepository bookmarkRepository, ILogger<BookmarkController> logger)
        {
            this.bookmarkRepository = bookmarkRepository;
            this.logger = logger;
        }

        // Get /bookmarks
        [HttpGet]
        public async Task<IEnumerable<BookmarkDto>> GetBookmarks()
        {
            var connectionId = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.TraceIdentifier))
            {
                var traceId = HttpContext.TraceIdentifier;
                var stop = traceId.IndexOf(":");
                connectionId = traceId.Substring(0, stop);
            }

            logger.LogInformation("Hit GetBookmarksAsync service: ");

            var bookmarks = (await bookmarkRepository.GetBookmarkItemsAsync())
                .Select(bookmarkItem => bookmarkItem.AsDto());

            return bookmarks;
        }


        // Get /bookmarks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkDto>> GetBookmarkItemById(Guid id)
        {
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
            var existingBookmark = await bookmarkRepository.GetBookmarkItemByIdAsync(id);

            if (existingBookmark is null)
            {
                return NotFound();
            }

           await bookmarkRepository.DeleteBookmarkItemAsync(id);

            return NoContent();

        }

        private void PrintHeader(IHeaderDictionary headerDictionary)
        {
            foreach(StringValues keys in headerDictionary.Keys)
            {
                string str = "Key: " + keys + " Values: : " + headerDictionary[keys];
                Console.WriteLine(str);
            }
        }

    }

}