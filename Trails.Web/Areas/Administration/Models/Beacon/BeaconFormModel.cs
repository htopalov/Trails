using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.Areas.Administration.Models.Beacon
{
    public class BeaconFormModel
    {
        [Required]
        [RegularExpression(
            ValidationConstants.ImeiPattern,
            ErrorMessage = ErrorMessages.InvalidImeiFormatError)]
        public string Imei { get; set; }

        [Required]
        [RegularExpression(
            ValidationConstants.PhonePattern,
            ErrorMessage = ErrorMessages.InvalidPhoneNumberFormatError)]
        public string SimCardNumber { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.DescriptionMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
