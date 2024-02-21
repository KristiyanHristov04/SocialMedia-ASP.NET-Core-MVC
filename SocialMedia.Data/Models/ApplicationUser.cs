using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.ApplicationUser;

namespace SocialMedia.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public ICollection<LikedPost> LikedPosts { get; set; } 
            = new List<LikedPost>();

        public ICollection<Post> Posts { get; set; }
      = new List<Post>();
    }
}
