using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data.Models
{
    public class LikedPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
