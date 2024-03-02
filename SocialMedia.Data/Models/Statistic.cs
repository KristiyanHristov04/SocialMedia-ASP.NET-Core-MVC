using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data.Models
{
    public class Statistic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReportedPostsDeletedCount { get; set; }

        [Required]
        public int AllTimeUsersCount { get; set; }
    }
}
