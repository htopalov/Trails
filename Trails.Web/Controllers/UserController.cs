using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Trails.Data.DomainModels;
using Trails.Models.User;
using Trails.Services.User;
using static Trails.Common.NotificationConstants;

namespace Trails.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;

        public UserController(
            UserManager<User> userManager,
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword() 
            => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([EmailAddress] string email)
        {
            if (!ModelState.IsValid)
            {
                TempData[TempDataKeyFail] = ForgotPasswordEmailFormatFail;
                return View();
            }

            var user = await this.userManager
                .FindByEmailAsync(email);

            if (user == null)
            {
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            var token = await this.userManager
                .GeneratePasswordResetTokenAsync(user);

            var link = Url.Action("ResetPassword", "User", 
                new { token, email = user.Email }, Request.Scheme);

            var message = this.emailService
                .PasswordResetMessage(email, link);

            var sendResult = await this.emailService
                .SendEmailAsync(message);

            if (sendResult)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            TempData[TempDataKeyFail] = EmailSendFail;
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
            => View();

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email) 
            => View(new ResetPassword { Token = token, Email = email });

        [AllowAnonymous]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await this.userManager
                .FindByEmailAsync(resetPassword.Email);

            if (user == null)
            {
                TempData[TempDataKeySuccess] = PasswordResetSuccess;
                return Redirect("/");
            }
            
            var resetPassResult = await this.userManager
                .ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                   
                return View();
            }

            TempData[TempDataKeySuccess] = PasswordResetSuccess;
            return Redirect("/");
        }
    }
}
