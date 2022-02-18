using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Trails.Models.ValidationAttributes;

namespace Trails.Models.Event
{
    public class EventImageEditModel
    {
        [Required]
        [ValidateImageExtension]
        public IFormFile Image { get; set; }
    }
}
