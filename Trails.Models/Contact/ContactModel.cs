using System.ComponentModel.DataAnnotations;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.Contact
{
    public class ContactModel
    {
        [Required]
        [StringLength(
            FullnameMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = FullnameMinLength)]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = InvalidEmailFormatError)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(
            PhonePattern,
            ErrorMessage = InvalidPhoneNumberFormatError)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(
            MessageMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = MessageMinLength)]
        public string Message { get; set; }
    }
}
