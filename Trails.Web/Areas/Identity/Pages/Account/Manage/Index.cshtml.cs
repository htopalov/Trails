#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trails.Web.Data.DomainModels;

using static Trails.Web.Data.DataValidationConstants.User;
using static Trails.Web.Areas.IdentityValidationConstants.RegisterModelErrorMessages;

namespace Trails.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(FirstNameMaxLength, ErrorMessage = StringLengthError, MinimumLength = FirstNameMinLength)]
            [Display(Name = "Firstname")]
            public string Firstname { get; set; }

            [Required]
            [StringLength(LastNameMaxLength, ErrorMessage = StringLengthError, MinimumLength = LastNameMinLength)]
            [Display(Name = "Lastname")]
            public string LastName { get; set; }

            [Required]
            [StringLength(CountryNameMaxLength, ErrorMessage = StringLengthError, MinimumLength = CountryNameMinLength)]
            [Display(Name = "Country")]
            public string CountryName { get; set; }

            [Required]
            [Range(MinAge, MaxAge, ErrorMessage = AgeRangeError)]
            [Display(Name = "Age")]
            public int Age { get; set; }

            [Required]
            [RegularExpression(PhonePattern, ErrorMessage = IncorrectPhoneNumberFormatError)]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            Input = mapper.Map<InputModel>(user);
            Username = user.UserName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager
                .GetUserAsync(User);

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            var user = await userManager
                .GetUserAsync(User);

            user = AssignUserProperties(user);

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var result = await userManager
                .UpdateAsync(user);

            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error while trying to update your data. Please try again.";
                return RedirectToPage();
            }

            await signInManager
                .RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private User AssignUserProperties(User user)
        {
            var userProps = typeof(User)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsValueType == true || p.PropertyType == typeof(string))
                .ToArray();

            for (int i = 0; i < userProps.Length; i++)
            {
                switch (userProps[i].Name)
                {
                    case "UserName":
                        user.UserName = user.UserName;
                        break;
                    case "FirstName":
                        user.FirstName = Input.Firstname;
                        break;
                    case "LastName":
                        user.LastName = Input.LastName;
                        break;
                    case "CountryName":
                        user.CountryName = Input.CountryName;
                        break;
                    case "Age":
                        user.Age = Input.Age;
                        break;
                    case "PhoneNumber":
                        user.PhoneNumber = Input.PhoneNumber;
                        break;
                    default: continue;
                }
            }

            return user;
        }
    }
}
