using System.ComponentModel.DataAnnotations;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.Route
{
    public class RouteEditFormModel : IRouteModel
    {
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
    }
}
