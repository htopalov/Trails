using System.ComponentModel.DataAnnotations;
using Trails.Common;

namespace Trails.Models.Route
{
    public class RouteCreateModel : IRouteModel
    {
        public RouteCreateModel() 
            => this.RoutePoints = new List<double[]>();

        [Required]
        [StringLength(
            ValidationConstants.RouteNameMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.RouteNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.NameMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.NameMinLength)]
        public string StartLocationName { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.NameMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.NameMinLength)]
        public string FinishLocationName { get; set; }

        [Range(0.0, double.MaxValue,
            ErrorMessage = ErrorMessages.RouteLengthError)]
        public double Length { get; set; }

        [Range(0.0, double.MaxValue,
            ErrorMessage = ErrorMessages.AltitudeInputError)]
        public double MinimumAltitude { get; set; }

        [Range(0.0, double.MaxValue,
            ErrorMessage = ErrorMessages.AltitudeInputError)]
        public double MaximumAltitude { get; set; }

        public List<double[]> RoutePoints { get; set; }

        public string EventId { get; set; }
    }
}
