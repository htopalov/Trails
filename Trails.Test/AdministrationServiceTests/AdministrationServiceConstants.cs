namespace Trails.Test.AdministrationServiceTests
{
    public static class AdministrationServiceConstants
    {
        public const int PageNumberWithLessEvents = 2;
        public const int ExpectedUnapprovedEventsCount = 1;
        public const int ExpectedPagedEventsCount = 0;
        public const string EventToBeApprovedId = "00000000-0000-0000-0000-000000000004";
        public const string EventWithInvalidId = "00000000-0000-0000-0000-000004440004";
        public const string EventToBeApprovedAndNotDeletedId = "00000000-0000-0000-0000-000000000002";
        public const string EventApprovedAndDeletedId = "00000000-0000-0000-0000-000000000003";
        public const string EventToDeclineId = "00000000-0000-0000-0000-000000000004";
        public const int EventsToPrepareCount = 1;
        public const string EventToPrepareId = "00000000-0000-0000-0000-000000000009";
        public const int ExpectedCountOfParticipantsToPrepare = 2;
        public const int ExpectedCountOfBeaconsAvailableToConnect = 2;
    }
}
