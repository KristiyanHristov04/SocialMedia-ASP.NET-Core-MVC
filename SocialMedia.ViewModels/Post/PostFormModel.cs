using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialMedia.Common.DataConstants.Post;
using Microsoft.AspNetCore.Http;

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
