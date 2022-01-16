namespace Trails.Web.Areas
{
    public class IdentityValidationConstants
    {
        public class InputModelErrorMessages
        {
            public const string EmailRequiredError = "Email is required";
            public const string EmailInvalidFormatError = "Email format not valid";
            public const string PasswordRequiredError = "Password is required";
            public const string ModelStateNullUserKey = "No user";
            public const string ModelStateNullUserValue = "User does not exist.";
            public const string ModelStateInvalidLoginValue = "Invalid login attempt.";
        }

        public class RegisterModelErrorMessages
        {
            public const string UsernameRequiredError = "Username is required";
            public const string StringLengthError = "{0} must be between {2} and {1} characters long.";
            public const string FirstNameRequiredError = "First name is required";
            public const string LastNameRequiredError = "Last name is required";
            public const string CountryNameRequired = "Country name is required";
            public const string AgeRequiredError = "Age is required";
            public const string AgeRangeError = "{0} must be between {1} and {2}.";
            public const string GenderRequiredError = "Gender is required";
            public const string GenderTypeError = "You are trying to select gender which is not listed!";
            public const string EmailRequiredError = "Email is required";
            public const string InvalidEmailFormatError = "Email format not valid";
            public const string PhoneNumberRequiredError = "Phone number is required";
            public const string IncorrectPhoneNumberFormatError = "Incorrect phone number format";
            public const string PasswordRequiredError = "Password is required";
            public const string ConfirmPasswordRequiredError = "Confirm password is required";
            public const string ComparePasswordsError = "The password and confirmation password do not match.";
        }
    }
}
