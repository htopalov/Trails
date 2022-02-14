using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.Models.Route
{
    public class RouteEditFormModel : IRouteModel
    {
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
    }
}
