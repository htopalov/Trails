﻿namespace Trails.Web.Common
{
    public static class ErrorMessages
    {
        public const string ModelStateNullUserKey = "No user";
        public const string ModelStateInvalidLoginError = "Invalid login attempt.";
        public const string StringLengthError = "{0} must be between {2} and {1} characters long.";
        public const string GenderTypeError = "Please select gender from the list.";
        public const string InvalidEmailFormatError = "Email format not valid.";
        public const string IncorrectPhoneNumberFormatError = "Incorrect phone number format.";
        public const string ComparePasswordsError = "The password and confirmation password do not match.";
    }
}
