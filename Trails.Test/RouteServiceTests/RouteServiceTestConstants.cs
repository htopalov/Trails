namespace Trails.Test.RouteServiceTests
{
    public static class RouteServiceTestConstants
    {
        public const string InvalidRouteId = "10000000-0000-0880-0990-003300000001";
        public const string ValidRouteId = "00000000-1111-2222-0000-000000000000";
        public const int ExpectedCountOfRoutePoints = 4;
        public const int ExpectedTotalRoutesCount = 2;
        public const string SearchTerm = "rOutE 2";
        public const int ExpectedCountOfRoutesWhenSearching = 1;
        public const int ExpectedCountOfRoutesWhenSearchingEmptyString = 2;
        public const int RoutesPerPage = 1;
        public const int ExpectedRoutesCountWithRoutesPerPage = 1;
    }
}
