namespace Trails.Test.BeaconServiceTests
{
    public static class BeaconServiceTestConstants
    {
        public const int CurrentPage = 2;
        public const int BeaconsPerPage = 5;
        public const string CorrectBeaconId = "00000000-0000-0000-0000-000000000001";
        public const string IncorrectBeaconId = "00000000-0000-0000-0000-000000000111";
        public const string ExistingBeaconId = "00000000-0000-0000-0000-000000000006";
        public const string NotInUseExistingBeaconId = "00000000-0000-0000-0000-000000000005";

        public const int ExpectedTotalBeaconsCount = 6;
        public const int ExpectedBeaconsPerPageCount = 5;
        public const int ExpectedBeaconsInPage = 1;
        public const string ExpectedBeaconImei = "000000000000001";
    }
}
