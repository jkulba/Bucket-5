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
        public async Task<IEnumerable<BookmarkDto>> GetBookmarksAsync()
        {
            logger.LogDebug("Hit GetBookmarksAsync service.");
            var bookmarks = (await bookmarkRepository.GetBookmarkItemsAsync())
                .Select(bookmarkItem => bookmarkItem.AsDto());
            return bookmarks;
        }


        // Get /bookmarks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkDto>> GetBookmarkAsync(Guid id)
        {
            var bookmark = await bookmarkRepository.GetBookmarkItemAsync(id);
            if (bookmark is null)
            {
                return NotFound();  
            }
            return bookmark.AsDto();
        }

        // POST /bookmarks
        [HttpPost]
        public async Task<ActionResult<BookmarkDto>> CreateBookmarkAsync(CreateBookmarkDto bookmarkDto)
        {
            BookmarkItem item = new()
            {
                Id = Guid.NewGuid(),
                Title = bookmarkDto.Title,
                Url = bookmarkDto.Url,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await bookmarkRepository.CreateBookmarkAsync(item);

            return CreatedAtAction(nameof(GetBookmarkAsync), new { id = item.Id}, item.AsDto());
        }

    }

}