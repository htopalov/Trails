using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Trails.Common;
using Trails.Data.DomainModels;
using Trails.Data.Enums;

namespace Trails.Models.Event
{
    public class EventFormModel : IEventModel
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
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [EnumDataType(typeof(EventType), 
            ErrorMessage = ErrorMessages.EventTypeError)]
        public int Type { get; set; }

        [Required]
        [EnumDataType(typeof(DifficultyLevel), 
            ErrorMessage = ErrorMessages.DifficultyLevelError)]
        public int DifficultyLevel { get; set; }

        [Range(1,double.MaxValue, 
            ErrorMessage = ErrorMessages.InvalidMinLengthError)]
        public double Length { get; set; }

        public string? CreatorId { get; set; }
        public User? Creator { get; set; }

        public IFormFile Image { get; set; }
    }
}
