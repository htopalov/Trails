using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static Trails.Common.NotificationConstants;

namespace Trails.Models.ValidationAttributes
{
    public class ValidateImageExtension : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Image is required.");
            }
            else if (!ValidateImage((IFormFile)value))
            {
                return new ValidationResult(ImageFileExtensionError);
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
