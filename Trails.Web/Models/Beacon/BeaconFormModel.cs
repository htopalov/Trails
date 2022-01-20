using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.Models.Beacon
{
    public class BeaconFormModel
    {
        [Required]
        [StringLength(
            ValidationConstants.ImeiLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.ImeiLength)]
        public string Imei { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.SimCardNumberLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.SimCardNumberLength)]
        public string SimCardNumber { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.SimCardNumberLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.SimCardNumberLength)]
        public string Description { get; set; }

        public string Key { get; set; }
    }
}
