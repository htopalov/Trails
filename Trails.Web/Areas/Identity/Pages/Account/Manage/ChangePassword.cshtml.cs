using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trails.Data.DomainModels;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;
using static Trails.Common.NotificationConstants;

namespace Trails.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public ChangePasswordModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(
                PasswordMaxLength,
                ErrorMessage = StringLengthError,
                MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = ComparePasswordsError)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await this.userManager
                .GetUserAsync(User);

            var changePasswordResult = await this.userManager
                .ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                TempData[TempDataKeyFail] = UserPasswordChangedFail;
                return Page();
            }

            await this.signInManager
                .RefreshSignInAsync(user);

            TempData[TempDataKeySuccess] = UserPasswordChangedSuccess;

            return RedirectToPage();
        }
    }
}