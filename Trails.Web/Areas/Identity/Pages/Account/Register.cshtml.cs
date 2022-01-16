#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trails.Web.Data.DomainModels;
using Trails.Web.Data.Enums;

using static Trails.Web.Data.DataConstants.User;

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
            [Required(ErrorMessage = "Username is required")]
            [StringLength(UsernameMaxLength, ErrorMessage = "Username must be between {2} and {1} characters long.", MinimumLength = UsernameMinLength)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required(ErrorMessage = "First name is required")]
            [StringLength(FirstNameMaxLength, ErrorMessage = "First name must be between {2} and {1} characters long.", MinimumLength = FirstNameMinLength)]
            [Display(Name = "Firstname")]
            public string Firstname { get; set; }

            [Required(ErrorMessage = "Last name is required")]
            [StringLength(LastNameMaxLength, ErrorMessage = "Last name must be between {2} and {1} characters long.", MinimumLength = LastNameMinLength)]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Country name is required")]
            [StringLength(CountryNameMaxLength, ErrorMessage = "Country name must be between {2} and {1} characters long.", MinimumLength = CountryNameMinLength)]
            [Display(Name = "Country")]
            public string CountryName { get; set; }

            [Required(ErrorMessage = "Age is required")]
            [Range(MinAge,MaxAge, ErrorMessage = "Age must be between {1} and {2}.")]
            [Display(Name = "Age")]
            public int Age { get; set; }

            [Required(ErrorMessage = "Gender is required")]
            [Display(Name = "Gender")]
            [EnumDataType(typeof(Gender), ErrorMessage = "You are trying to select gender which is not listed!")]
            public int Gender { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Email format not valid")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Phone number is required")]
            [RegularExpression(PhonePattern, ErrorMessage = "Phone number format not correct")]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [StringLength(PasswordMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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
