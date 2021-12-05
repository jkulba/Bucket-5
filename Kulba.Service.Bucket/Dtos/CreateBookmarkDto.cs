using System.ComponentModel.DataAnnotations;

namespace Kulba.Service.Bucket.Dtos
{
    public record CreateBookmarkDto 
    {
        [Required]
        public string Title { get; init; }

        [Required]
        public string Url { get; init; }
    }
}