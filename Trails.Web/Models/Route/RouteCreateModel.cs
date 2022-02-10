using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;
using Trails.Web.Data.DomainModels;

namespace Trails.Web.Models.Route
{
    public class RouteCreateModel
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

        public double MinimumAltitude { get; set; }

        public double MaximumAltitude { get; set; }

        public List<double[]> RoutePoints { get; set; }

        public string EventId { get; set; }
    }
}
