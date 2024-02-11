using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.Email;
namespace SocialMedia.ViewModels.Home
{
    public class ContactFormModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string FromEmail { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = null!;

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Message { get; set; } = null!;
    }
}
