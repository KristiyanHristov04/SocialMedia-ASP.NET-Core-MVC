using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Areas.Admin.ViewModels.Announcement
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PublishDate { get; set; } = null!;

        public string? UserUsername { get; set; }
    }
}
