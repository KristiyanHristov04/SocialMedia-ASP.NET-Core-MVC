using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Common.ValidationAttributes
{
    public class FileExtensionValidationAttribute : ValidationAttribute
    {
        HashSet<string> allowedFilesExtensions = new HashSet<string>()
        {
            "jpeg",
            "jpg",
            "png",
            "gif",
            "mp4"
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value!;

            string currentFileExtension
                = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);

            if (!allowedFilesExtensions.Contains(currentFileExtension))
            {
                return new ValidationResult($"Extension .{currentFileExtension} is not supported!");
            }

            return ValidationResult.Success;
        }
    }
}
