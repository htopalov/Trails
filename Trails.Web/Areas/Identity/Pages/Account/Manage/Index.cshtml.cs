using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AgeCalculator.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trails.Data.DomainModels;
using Trails.Web.Areas.Identity.Pages.Account.Contracts;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;
using static Trails.Common.NotificationConstants;

namespace Trails.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }

        public int Age { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : IUserInputModel
        {
            [Required]
            [StringLength(
                FirstNameMaxLength,
                ErrorMessage = StringLengthError,
                MinimumLength = FirstNameMinLength)]
            [Display(Name = "Firstname")]
            public string Firstname { get; set; }

            [Required]
            [StringLength(
                LastNameMaxLength,
                ErrorMessage = StringLengthError,
                MinimumLength = LastNameMinLength)]
            [Display(Name = "Lastname")]
            public string LastName { get; set; }

            [Required]
            [StringLength(
                CountryNameMaxLength,
                ErrorMessage = StringLengthError,
                MinimumLength = CountryNameMinLength)]
            [Display(Name = "Country")]
            public string CountryName { get; set; }

            [Required]
            [RegularExpression(
                PhonePattern,
                ErrorMessage = InvalidPhoneNumberFormatError)]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var input = new InputModel
            {
                Firstname = user.FirstName,
                LastName = user.LastName,
                CountryName = user.CountryName,
                PhoneNumber = user.PhoneNumber,
            };

            Input = input;
            Username = user.UserName;
            Age = CalculateAge(user.BirthDate);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager
                .GetUserAsync(User);

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            var user = await this.userManager
                .GetUserAsync(User);

            user = AssignUserProperties(user);

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var result = await this.userManager
                .UpdateAsync(user);

            if (!result.Succeeded)
            {
                TempData[TempDataKeyFail] = UserProfileEditFail;
                return RedirectToPage();
            }

            await this.signInManager
                .RefreshSignInAsync(user);

            TempData[TempDataKeySuccess] = UserProfileEditSuccess;
            return RedirectToPage();
        }

        private User AssignUserProperties(User user)
        {
            //get only properties of entity User which should be edited if needed
            var userProps = typeof(User)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p=> p.DeclaringType == typeof(User) &&
                           p.PropertyType == typeof(string) ||
                           p.Name == "PhoneNumber")
                .ToArray();

            for (int i = 0; i < userProps.Length; i++)
            {
                switch (userProps[i].Name)
                {
                    case "FirstName":
                        user.FirstName = Input.Firstname;
                        break;
                    case "LastName":
                        user.LastName = Input.LastName;
                        break;
                    case "CountryName":
                        user.CountryName = Input.CountryName;
                        break;
                    case "PhoneNumber":
                        user.PhoneNumber = Input.PhoneNumber;
                        break;
                    default: continue;
                }
            }

            return user;
        }

        private static int CalculateAge(DateTime birthDate) 
            => birthDate.CalculateAge(DateTime.Today).Years;
    }
}
