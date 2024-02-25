using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SocialMedia.Common.DataConstants.Post;

namespace SocialMedia.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        public string Path { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        //Navigation Property
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public ICollection<LikedPost> LikedPosts { get; set; }
              = new List<LikedPost>();

        public ICollection<ReportPost> ReportPosts { get; set; }
             = new List<ReportPost>();
    }
}
