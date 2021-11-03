using Kulba.Service.Bucket.Dtos;
using Kulba.Service.Bucket.Entities;

namespace Kulba.Service.Bucket
{
    public static class Extensions
    {
        public static BookmarkDto AsDto(this BookmarkItem bookmarkItem)
        {
            return new BookmarkDto
            {
                Id = bookmarkItem.Id,
                Title = bookmarkItem.Title,
                Url = bookmarkItem.Url,
                CreatedDate = bookmarkItem.CreatedDate
            };
        }
    }
}