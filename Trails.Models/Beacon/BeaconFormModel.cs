using System.ComponentModel.DataAnnotations;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.Beacon
{
    public class BeaconFormModel : IBeaconModel
    {
        [Required]
        [RegularExpression(
            ImeiPattern,
            ErrorMessage = InvalidImeiFormatError)]
        public string Imei { get; set; }

        [Required]
        [RegularExpression(
            PhonePattern,
            ErrorMessage = InvalidPhoneNumberFormatError)]
        public string SimCardNumber { get; set; }

        [Required]
        [StringLength(
            DescriptionMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
