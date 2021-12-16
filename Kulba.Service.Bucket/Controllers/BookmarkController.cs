using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulba.Service.Bucket.Dtos;
using Kulba.Service.Bucket.Entities;
using Kulba.Service.Bucket.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            logger.LogDebug("Hit GetBookmarksAsync service.");
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

    }

}