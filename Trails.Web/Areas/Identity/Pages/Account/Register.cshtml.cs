#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trails.Web.Data.DomainModels;
using Trails.Web.Data.Enums;

using static Trails.Web.Data.DataValidationConstants.User;
using static Trails.Web.Areas.IdentityValidationConstants.RegisterModelErrorMessages;

namespace Trails.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public RegisterModel(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = UsernameRequiredError)]
            [StringLength(UsernameMaxLength, ErrorMessage = StringLengthError, MinimumLength = UsernameMinLength)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required(ErrorMessage = FirstNameRequiredError)]
            [StringLength(FirstNameMaxLength, ErrorMessage = StringLengthError, MinimumLength = FirstNameMinLength)]
            [Display(Name = "Firstname")]
            public string Firstname { get; set; }

            [Required(ErrorMessage = LastNameRequiredError)]
            [StringLength(LastNameMaxLength, ErrorMessage = StringLengthError, MinimumLength = LastNameMinLength)]
            [Display(Name = "Lastname")]
            public string LastName { get; set; }

            [Required(ErrorMessage = CountryNameRequired)]
            [StringLength(CountryNameMaxLength, ErrorMessage = StringLengthError, MinimumLength = CountryNameMinLength)]
            [Display(Name = "Country")]
            public string CountryName { get; set; }

            [Required(ErrorMessage = AgeRequiredError)]
            [Range(MinAge,MaxAge, ErrorMessage = AgeRangeError)]
            [Display(Name = "Age")]
            public int Age { get; set; }

            [Required(ErrorMessage = GenderRequiredError)]
            [Display(Name = "Gender")]
            [EnumDataType(typeof(Gender), ErrorMessage = GenderTypeError)]
            public int Gender { get; set; }

            [Required(ErrorMessage = EmailRequiredError)]
            [EmailAddress(ErrorMessage = InvalidEmailFormatError)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = PhoneNumberRequiredError)]
            [RegularExpression(PhonePattern, ErrorMessage = IncorrectPhoneNumberFormatError)]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = PasswordRequiredError)]
            [StringLength(PasswordMaxLength, ErrorMessage = StringLengthError, MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = ConfirmPasswordRequiredError)]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = ComparePasswordsError)]
            public string ConfirmPassword { get; set; }
        }


        public void OnGet(string returnUrl = null) 
            => ReturnUrl = returnUrl;

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Username,
                    FirstName = Input.Firstname,
                    LastName = Input.LastName,
                    CountryName = Input.CountryName,
                    Age = Input.Age,
                    Gender = (Gender)Input.Gender,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber
                };

                var result = await this.userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
