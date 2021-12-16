using System.ComponentModel.DataAnnotations;

namespace Kulba.Service.Bucket
{
    public record UpdateBookmarkDto
    {
        [Required]
        public string Title { get; init; }

        [Required]
        public string Url { get; init; }
    }
}