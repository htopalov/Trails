using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Trails.Common;

namespace Trails.Models.ValidationAttributes
{
    public class ValidateImageExtension : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!ValidateImage((IFormFile)value))
            {
                return new ValidationResult(ErrorMessages.ImageFileExtensionError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        private bool ValidateImage(IFormFile imgFile)
        {
            string[] extensions = new[] { "jpg", "jpeg", "png" };

            var extension = Path.GetExtension(imgFile.FileName).TrimStart('.');

            return extensions.Any(e => e.EndsWith(extension));
        }
    }
}
