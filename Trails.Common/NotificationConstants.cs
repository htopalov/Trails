namespace Trails.Common
{
    public static class NotificationConstants
    {
        public const string TempDataKeySuccess = "Success";
        public const string TempDataKeyFail = "Fail";

        public const string UserPasswordChangedSuccess = "Your password has been changed.";
        public const string UserPasswordChangedFail = "Something went wrong. Please trry again.";
        public const string UserProfileEditSuccess = "Your profile has been updated";
        public const string UserProfileEditFail = "Unexpected error while trying to update your data. Please try again.";
        public const string ForgotPasswordEmailFormatFail = "Please provide valid email.";
        public const string EmailSendFail = "Email with reset instruction could not be send.";
        public const string BeaconExists = "Beacon already exists.";
        public const string BeaconCreated = "Beacon created successfully.";
        public const string BeaconNotExistingOrInUse = "Beacon does not exist or it's currently in use.";
        public const string BeaconDeletedSuccess = "Beacon deleted successfully.";
        public const string BeaconEditedSuccess = "Beacon edited successfully.";
        public const string EventExists = "Event with the same name already exists.";
        public const string RouteCreateError = "Either a route with the same name already exists or event does not exist.";
        public const string RouteCreateSuccess = "Route for your event created successfully.";
        public const string EventCreateSuccess = "Event awaiting approval by admin.";
        public const string EventDeleteSuccess = "Event deleted successfully.";
        public const string EventDeleteFail = "Event could not be deleted.";
        public const string MissingRoutePropertiesError = "Please draw or import route and fill all fields.";
        public const string ParticipantAlreadyAppliedError = "You have already applied for this event or missed deadline.";
        public const string ParticipantApplicationSuccess = "Successfully applied for event.";
        public const string ParticipantApproveError = "Participant could not be approved for event.";
        public const string EventImageEditSuccess = "Image changed successfully.";
        public const string EventImageEditError = "Image could not be updated.";
        public const string EventEditSuccess = "Event edited successfully.";
        public const string EventEditFail = "Event could not be updated.";
        public const string RouteEditSuccess = "Route edited successfully.";
        public const string RouteEditFail = "Route could not be edited.";
        public const string EventApproveFail = "Event could not be approved.";
        public const string EventApproveSuccess = "Event approved successfully.";
        public const string EventDeclineFail = "Event could not be removed.";
        public const string EventDeclineSuccess = "Event removed successfully.";
        public const string ImageFileExtensionError = "Allowed type of images: jpg, jpeg, png";
        public const string ContactSuccess = "Your message was send.";
        public const string ContactFail = "Your message could not be send.";
    }
}
