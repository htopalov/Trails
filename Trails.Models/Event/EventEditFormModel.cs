using System.ComponentModel.DataAnnotations;
using Trails.Common;
using Trails.Data.Enums;
using Trails.Models.ValidationAttributes;

namespace Trails.Models.Event
{
    public class EventEditFormModel : IEventModel
    {
        [Required]
        [StringLength(
            ValidationConstants.EventNameMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.EventNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.DescriptionMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.DescriptionMinLength)]
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
            ErrorMessage = ErrorMessages.EventTypeError)]
        public EventType Type { get; set; }

        [Required]
        [EnumDataType(typeof(DifficultyLevel),
            ErrorMessage = ErrorMessages.DifficultyLevelError)]
        public DifficultyLevel DifficultyLevel { get; set; }

        [Range(1, double.MaxValue,
            ErrorMessage = ErrorMessages.InvalidMinLengthError)]
        public double Length { get; set; }

        public bool IsModifiedByCreator { get; set; }
    }
}
