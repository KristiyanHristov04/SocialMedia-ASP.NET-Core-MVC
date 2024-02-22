using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.ViewModels.Post
{
    public class ProfileViewModel
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int TotalPosts { get; set; }
        public string CountryName { get; set; } = null!;
    }
}
