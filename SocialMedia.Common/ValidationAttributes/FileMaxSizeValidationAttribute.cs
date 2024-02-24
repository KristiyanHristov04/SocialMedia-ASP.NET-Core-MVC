using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static SocialMedia.Common.DataConstants.FileConfiguration;

namespace SocialMedia.Common.ValidationAttributes
{
    public class FileMaxSizeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value!;

            if (file.Length > MaxAllowedFileSize)
            {
                return new ValidationResult($"Max file size is {MaxAllowedFileSize / 1000000} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
