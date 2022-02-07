namespace Trails.Web.Common
{
    public static class NotificationConstants
    {
        public const string TempDataKeySuccess = "Success";
        public const string TempDataKeyFail = "Fail";
        public const string TempDataKeyWarning = "Warning";

        public const string UserPasswordChangedSuccess = "Your password has been changed.";
        public const string UserPasswordChangedFail = "Something went wrong. Please trry again.";
        public const string UserProfileEditSuccess = "Your profile has been updated";
        public const string UserProfileEditFail = "Unexpected error while trying to update your data. Please try again.";
        public const string BeaconExists = "Beacon already exists.";
        public const string BeaconCreated = "Beacon created successfully.";
        public const string BeaconNotExisting = "Beacon you tried to delete does not exist.";
        public const string BeaconDeletedSuccess = "Beacon deleted successfully.";
        public const string BeaconEditedSuccess = "Beacon edited successfully.";
        public const string BeaconEditedFail = "Beacon is either not existing or it has the same parameters as the input.";
        public const string EventExists = "Event with the same name already exists.";
        public const string RouteCreateError = "Either a route with the same name already exists or event does not exist.";
        public const string EventCreateSuccess = "Event created successfully.";
        public const string MissingEventImageError = "Please provide image for event.";
        public const string MissingRoutePropertiesError = "Please draw or import route and fill all fields.";
    }
}
