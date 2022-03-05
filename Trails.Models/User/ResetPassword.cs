using System.ComponentModel.DataAnnotations;
using static Trails.Common.ErrorMessages;
using static Trails.Common.ValidationConstants;

namespace Trails.Models.User
{
    public class ResetPassword
    {
        [Required]
        [StringLength(
            PasswordMaxLength,
            ErrorMessage = StringLengthError,
            MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = ComparePasswordsError)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
