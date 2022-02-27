using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Trails.Data.Enums;
using Trails.Models.ValidationAttributes;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.Event
{
    public class EventFormModel : IEventModel
    {
        [Required]
        [StringLength(
            EventNameMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = EventNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            DescriptionMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [ValidateEventDate]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [EnumDataType(typeof(EventType), 
            ErrorMessage = EventTypeError)]
        public int Type { get; set; }

        [Required]
        [EnumDataType(typeof(DifficultyLevel), 
            ErrorMessage = DifficultyLevelError)]
        public int DifficultyLevel { get; set; }

        [Range(EventMinLength, double.MaxValue, 
            ErrorMessage = InvalidMinLengthError)]
        public double Length { get; set; }

        public string CreatorId { get; set; }

        [Required]
        [ValidateImageExtension]
        public IFormFile Image { get; set; }
    }
}
