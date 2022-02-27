namespace Trails.Common
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
        public const string InvalidStartEndDate = "End of the event must be after its start.";
        public const string EventThreeDaysBeforeStartError =
            "Event must be created or updated atleast three days before it's start.";
        public const string RouteLengthError = "Route length must be positive number.";
        public const string AltitudeInputError = "Altitude must be positive number.";
    }
}
