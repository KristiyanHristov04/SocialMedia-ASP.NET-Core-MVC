using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        [Required]
        public DateTime RegistrationDate { get; set; }

        public ICollection<LikedPost> LikedPosts { get; set; } 
            = new List<LikedPost>();

        public ICollection<Post> Posts { get; set; }
            = new List<Post>();

        public ICollection<Announcement> Announcements { get; set; }
            = new List<Announcement>();
    }
}
