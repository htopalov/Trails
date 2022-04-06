namespace Trails.Test.Web.EventController
{
    public static class EventControllerTestConstants
    {
        public const string ExpectedCreateEventHeading = "Add Event";
        public const string ExpectedDescriptionError = "The Description field is required.";
        public const string ExpectedStartDateError = "Event must be created or updated atleast three days before it's start.";
        public const string ExpectedDifficultyLevelError = "Please select difficulty level from the list.";
        public const string ExpectedImageError = "The Image field is required.";
    }
}
