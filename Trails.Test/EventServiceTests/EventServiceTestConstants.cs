namespace Trails.Test.EventServiceTests
{
    public static class EventServiceTestConstants
    {
        public const string InvalidEventId = "000011111100000011111";
        public const string ValidEventId = "00000000-0000-0000-0000-000000000004";
        public const string ExpectedFullNameOfCreator = "Georgi Petrov";
        public const string ValidFutureEventId = "00000000-0000-0000-0000-000000000003";
        public const string ValidExpiredEventId = "00000000-0000-0000-0000-000000000004";
        public const string ValidLiveEventId = "00000000-0000-0000-0000-000000000005";
        public const string ValidUserId = "10000000-0000-0000-0000-000000000001";
        public const string InvalidUserId = "123456789011111";
        public const string ValidExistingParticipantUserId = "30000000-0000-0000-0000-000000000003";
        public const string ValidExistingParticipantId = "00000000-0000-0000-0000-000000000444";
        public const string InvalidParticipantId = "00055066-7700-0000-0000-000000000333";
        public const string ValidParticipantIdAwaitingApprove = "00000000-0000-0000-0000-000000000999";
        public const int ExpectedCountOfClosestEvents = 2;
        public const int ExpectedCountOfPublicEvents = 4;
        public const int ExpectedCountOfPersonalEvents = 4;
        public const int ExpectedCountOfResultAfterSearch = 1;
        public const string SearchTerm = "evEnt 2";
        public const int CurrentPage = 1;
        public const int EventsPerPage = 2;
        public const int ExpectedCountOfEventsForPage = 2;
        public const int ExpectedCountOfLiveEvents = 1;
        public const int ExpectedCountOfParticipants = 1;
        public const int ExpectedCountOfRoutePoints = 3;
        public const string ParticipantIdInLiveEvent = "00000000-0000-0000-0000-000000000666";
        public const int ExpectedCountOfBeaconData = 2;
        public const string ValidParticipantIdFromPassedEvent = "00000000-0000-0000-0000-000000000444";
    }
}
