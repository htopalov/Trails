namespace Trails.Test.Web.UserController
{
    public static class UserControllerTestConstants
    {
        public const string ExpectedHeadingMessage = "Please check your inbox to reset your password.";
        public const string ExpectedErrorNotificationMessage = "Please provide valid email.";
        public const string ExpectedHeaderForPasswordReset =
            "<h2 class=\"page-section-heading text-center text-uppercase text-secondary mt-5\">Reset Password</h2>";
        public const string ExpectedSuccessNotificationMessage = "You password has been changed.";
        public const string ExpectedValidationError = "The password and confirmation password do not match.";
    }
}
