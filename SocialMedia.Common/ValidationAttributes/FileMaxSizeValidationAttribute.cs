using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Common.ValidationAttributes
{
    public class FileMaxSizeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value!;

            if (file.Length > 3000000)
            {
                return new ValidationResult($"Max file size is 3 MB.");
            }

            return ValidationResult.Success;
        }
    }
}
