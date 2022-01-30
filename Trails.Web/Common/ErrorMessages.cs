﻿namespace Trails.Web.Common
{
    public static class ErrorMessages
    {
        public const string ModelStateNullUserKey = "No user";
        public const string ModelStateInvalidLoginError = "Invalid login attempt.";
        public const string StringLengthError = "{0} must be between {2} and {1} characters long.";
        public const string GenderTypeError = "Please select gender from the list.";
        public const string InvalidEmailFormatError = "Email format not valid.";
        public const string InvalidPhoneNumberFormatError = "Incorrect phone number format.";
        public const string ComparePasswordsError = "The password and confirmation password do not match.";
        public const string InvalidImeiFormatError = "Imei format not valid.";
        public const string InvalidMinLengthError = "Length must be 1 or more kilometers.";
        public const string EventTypeError = "Please select event type from the list.";
        public const string DifficultyLevelError = "Please select difficulty level from the list.";
        public const string ImageFileExtensionError = "Allowed type of images: jpg, jpeg, png";
        public const string InvalidStartEndDate = "End of the event must be after its start.";
    }
}
