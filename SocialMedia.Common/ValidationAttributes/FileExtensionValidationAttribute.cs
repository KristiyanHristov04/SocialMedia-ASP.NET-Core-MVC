using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.FileConfiguration;

namespace SocialMedia.Common.ValidationAttributes
{
    public class FileExtensionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("File extension cannot be processed!");
            }

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
