namespace Trails.Test.Web.HomeController
{
    public static class HomeControllerTestConstants
    {
        public const string ExpectedHomeControllerParagraphFromAnonymousPartial =
            "<p class=\"masthead-subheading font-weight-light mb-0\">Trail running - Offroad Cycling - Orienteering - Speed Hiking - More...</p>";

        public const string ExpectedHeadingInLoggedUserPartial =
            "<h2 class=\"page-section-heading text-center text-uppercase text-secondary mb-5 mt-5\">Coming up events</h2>";

        public const string ExpectedHeadingInFaqView = 
            "<h1 class=\"page-section-heading text-center text-uppercase text-secondary mb-5 mt-5\">Frequently asked questions</h1>";

        public const string ExpectedHeadingInErrorView = 
            "<h3 class=\"page-section-heading text-center text-uppercase text-secondary mb-5 mt-5\">Please try again later :)</h3>";

        public const string ExpectedHeadingInContactPage = 
            "<h2 class=\"page-section-heading text-center text-uppercase text-secondary mb-2 mt-5\">Contact Us</h2>";

        public const string ExpectedMessageNotificationAfterContact = "Your message was send.";

        public const string ExpectedErrorForFullname = "The Fullname field is required.";

        public const string ExpectedErrorForMessage = "Message must be between 20 and 2000 characters long.";

        public const string ExpectedErrorForPhoneFormat = "Incorrect phone number format.";

        public const string ExpectedErrorMessageInvalidLogin = "Invalid login attempt.";

        public const string ExpectedHeaderWhenLogged = "Coming up events";
    }
}
