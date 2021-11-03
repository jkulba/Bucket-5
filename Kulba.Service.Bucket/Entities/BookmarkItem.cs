using System;

namespace Kulba.Service.Bucket.Entities
{
    public record BookmarkItem
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Url { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}