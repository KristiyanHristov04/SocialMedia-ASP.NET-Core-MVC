using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.Announcement;

namespace SocialMedia.Areas.Admin.ViewModels.Announcement
{
    public class AnnouncementFormModel
    {
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
    }
}
