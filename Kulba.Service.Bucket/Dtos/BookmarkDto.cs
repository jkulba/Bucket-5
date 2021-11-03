using System;

namespace Kulba.Service.Bucket.Dtos
{
    public record BookmarkDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Url { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}