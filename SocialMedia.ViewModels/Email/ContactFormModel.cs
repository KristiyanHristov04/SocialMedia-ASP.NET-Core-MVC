using System.ComponentModel.DataAnnotations;
namespace SocialMedia.ViewModels.Email
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
        public string Message { get; set; } = null!;
    }
}
