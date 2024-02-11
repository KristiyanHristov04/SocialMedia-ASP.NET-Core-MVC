using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.Post;

namespace SocialMedia.ViewModels.Post
{
    public class PostFormModel
    {
        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
