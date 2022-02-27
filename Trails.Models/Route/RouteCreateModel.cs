using System.ComponentModel.DataAnnotations;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.Route
{
    public class RouteCreateModel : IRouteModel
    {
        public RouteCreateModel() 
            => this.RoutePoints = new List<double[]>();

        [Required]
        [StringLength(
            RouteNameMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = RouteNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            NameMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = NameMinLength)]
        public string StartLocationName { get; set; }

        [Required]
        [StringLength(
            NameMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = NameMinLength)]
        public string FinishLocationName { get; set; }

        [Range(RouteMinLength, double.MaxValue,
            ErrorMessage = RouteLengthError)]
        public double Length { get; set; }

        [Range(MinAltitude, double.MaxValue,
            ErrorMessage = AltitudeInputError)]
        public double MinimumAltitude { get; set; }

        [Range(MinAltitude, double.MaxValue,
            ErrorMessage = AltitudeInputError)]
        public double MaximumAltitude { get; set; }

        public List<double[]> RoutePoints { get; set; }

        public string EventId { get; set; }
    }
}
