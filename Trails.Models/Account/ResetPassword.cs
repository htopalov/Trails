using System.ComponentModel.DataAnnotations;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.Account
{
    public class ResetPassword
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = ComparePasswordsError)]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
