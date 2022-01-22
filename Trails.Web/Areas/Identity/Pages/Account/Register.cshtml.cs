﻿#nullable disable

using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trails.Web.Areas.Identity.Pages.Account.Contracts;
using Trails.Web.Common;
using Trails.Web.Data.DomainModels;
using Trails.Web.Data.Enums;

namespace Trails.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel : IUserInputModel
        {
            [Required]
            [StringLength(
                ValidationConstants.UsernameMaxLength,
                ErrorMessage = ErrorMessages.StringLengthError,
                MinimumLength = ValidationConstants.UsernameMinLength)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [StringLength(
                ValidationConstants.FirstNameMaxLength,
                ErrorMessage = ErrorMessages.StringLengthError,
                MinimumLength = ValidationConstants.FirstNameMinLength)]
            [Display(Name = "Firstname")]
            public string Firstname { get; set; }

            [Required]
            [StringLength(
                ValidationConstants.LastNameMaxLength,
                ErrorMessage = ErrorMessages.StringLengthError,
                MinimumLength = ValidationConstants.LastNameMinLength)]
            [Display(Name = "Lastname")]
            public string LastName { get; set; }

            [Required]
            [StringLength(
                ValidationConstants.CountryNameMaxLength,
                ErrorMessage = ErrorMessages.StringLengthError,
                MinimumLength = ValidationConstants.CountryNameMinLength)]
            [Display(Name = "Country")]
            public string CountryName { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "BirthDate")]
            public DateTime BirthDate { get; set; }

            [Required]
            [Display(Name = "Gender")]
            [EnumDataType(typeof(Gender), ErrorMessage = ErrorMessages.GenderTypeError)]
            public int Gender { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmailFormatError)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [RegularExpression(
                ValidationConstants.PhonePattern, 
                ErrorMessage = ErrorMessages.InvalidPhoneNumberFormatError)]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(
                ValidationConstants.PasswordMaxLength,
                ErrorMessage = ErrorMessages.StringLengthError,
                MinimumLength = ValidationConstants.PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = ErrorMessages.ComparePasswordsError)]
            public string ConfirmPassword { get; set; }
        }


        public void OnGet(string returnUrl = null) 
            => ReturnUrl = returnUrl;

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = this.mapper
                .Map<User>(Input);

            var result = await this.userManager
                .CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await this.signInManager
                    .SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
