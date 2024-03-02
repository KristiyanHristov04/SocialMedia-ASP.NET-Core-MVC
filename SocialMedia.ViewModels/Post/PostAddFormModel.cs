using Microsoft.AspNetCore.Http;
using SocialMedia.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.Post;

namespace SocialMedia.ViewModels.Post
{
    public class PostAddFormModel
    {
        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        [FileExtensionValidation]
        [FileMaxSizeValidation]
        public IFormFile File { get; set; } = null!;
    }
}
