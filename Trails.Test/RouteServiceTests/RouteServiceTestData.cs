using System;
using System.Collections.Generic;
using Trails.Data.DomainModels;
using Trails.Data.Enums;
using Trails.Models.Route;

namespace Trails.Test.RouteServiceTests
{
    public static class RouteServiceTestData
    {
        public static List<Route> GetTestRoutes() 
            => new()
            {
                new ()
                {
                    Id = "00000000-1111-2222-0000-000000000000",
                    Name = "Valid test route 1",
                    StartLocationName = "Start line",
                    FinishLocationName = "Finish line",
                    Length = 20,
                    MinimumAltitude = 333,
                    MaximumAltitude = 1234.5,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    EventId = "00000000-0000-0000-0000-000000000001",
                },
                new()
                {
                    Id = "00000000-3333-4444-0000-000000000000",
                    Name = "Valid test route 2",
                    StartLocationName = "Start line",
                    FinishLocationName = "Finish line",
                    Length = 67,
                    MinimumAltitude = 456,
                    MaximumAltitude = 1878.9,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    EventId = "00000000-0000-0000-0000-000000000002",
                }
            };

        public static List<Event> GetTestEvents()
            => new()
            {
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Name = "Test event 1",
                    Description = "Description for event",
                    Length = 14.3,
                    StartDate = DateTime.UtcNow.AddDays(5),
                    EndDate = DateTime.UtcNow.AddDays(6),
                    Type = EventType.Cycling,
                    DifficultyLevel = DifficultyLevel.Demanding,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    Name = "Test event 2",
                    Description = "Description for event",
                    Length = 54.3,
                    StartDate = DateTime.UtcNow.AddDays(10),
                    EndDate = DateTime.UtcNow.AddDays(11),
                    Type = EventType.Running,
                    DifficultyLevel = DifficultyLevel.Extreme,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                }
            };

        public static List<RoutePoint> GetTestRoutePoints()
            => new()
            {
                new()
                {
                    Id = "11111111-0000-0000-0000-000000000000",
                    OrderNumber = 1,
                    Latitude = 42.25,
                    Longitude = 25.67,
                    Altitude = 456,
                    RouteId = "00000000-1111-2222-0000-000000000000"
                },
                new()
                {
                    Id = "22222222-0000-0000-0000-000000000000",
                    OrderNumber = 2,
                    Latitude = 42.27,
                    Longitude = 25.63,
                    Altitude = 434,
                    RouteId = "00000000-1111-2222-0000-000000000000"
                },
                new()
                {
                    Id = "33333333-0000-0000-0000-000000000000",
                    OrderNumber = 3,
                    Latitude = 42.28,
                    Longitude = 25.53,
                    Altitude = 420,
                    RouteId = "00000000-1111-2222-0000-000000000000"
                },
                new()
                {
                    Id = "44444444-0000-0000-0000-000000000000",
                    OrderNumber = 4,
                    Latitude = 42.29,
                    Longitude = 25.525678,
                    Altitude = 410.2,
                    RouteId = "00000000-1111-2222-0000-000000000000"
                },
                new()
                {
                    Id = "55555555-0000-0000-0000-000000000000",
                    OrderNumber = 1,
                    Latitude = 45.57,
                    Longitude = 23.49255,
                    Altitude = 310.3,
                    RouteId = "00000000-3333-4444-0000-000000000000"
                },
                new()
                {
                    Id = "66666666-0000-0000-0000-000000000000",
                    OrderNumber = 2,
                    Latitude = 45.64,
                    Longitude = 23.5000,
                    Altitude = 312.3,
                    RouteId = "00000000-3333-4444-0000-000000000000"
                },
                new()
                {
                    Id = "77777777-0000-0000-0000-000000000000",
                    OrderNumber = 3,
                    Latitude = 45.79,
                    Longitude = 23.23,
                    RouteId = "00000000-3333-4444-0000-000000000000"
                }
            };

        public static RouteCreateModel GetValidTestRouteCreateModelWithoutAltitudes()
            => new()
            {
                Name = "Test create model without altitudes in route points",
                StartLocationName = "Start location area",
                FinishLocationName = "Finish location area",
                Length = 13.4,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                EventId = "00000000-0000-0000-0000-000000000002",
                RoutePoints = new List<double[]>
                {
                    new []{42.34,22.67},
                    new []{42.35,22.68},
                    new []{42.36,22.69},
                    new []{42.67,23.21}
                }
            };

        public static RouteCreateModel GetValidTestRouteCreateModelWithAltitudes()
            => new()
            {
                Name = "Test create model with altitudes in route points",
                StartLocationName = "Start location area",
                FinishLocationName = "Finish location area",
                Length = 13.4,
                MinimumAltitude = 323,
                MaximumAltitude = 1234,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                EventId = "00000000-0000-0000-0000-000000000001",
                RoutePoints = new List<double[]>
                {
                    new []{42.34,22.67,1234.6},
                    new []{42.35,22.68, 1237.9},
                    new []{42.36,22.69, 1300},
                    new []{42.67,23.21, 1301.2}
                }
            };

        public static RouteCreateModel GetValidTestRouteCreateModelWithExistingName()
            => new()
            {
                Name = "Valid test route 1",
                StartLocationName = "Start location area",
                FinishLocationName = "Finish location area",
                Length = 13.4,
                MinimumAltitude = 323,
                MaximumAltitude = 1234,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                EventId = "00000000-0000-0000-0000-000000000001",
                RoutePoints = new List<double[]>
                {
                    new []{42.34,22.67,1234.6},
                    new []{42.35,22.68, 1237.9},
                    new []{42.36,22.69, 1300},
                    new []{42.67,23.21, 1301.2}
                }
            };

        public static RouteCreateModel InvalidTestRouteCreateModelWithInvalidEventId()
            => new()
            {
                Name = "Valid test route 1",
                StartLocationName = "Start location area",
                FinishLocationName = "Finish location area",
                Length = 13.4,
                MinimumAltitude = 323,
                MaximumAltitude = 1234,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                EventId = "90075432-0000-0000-0000-000000000001",
                RoutePoints = new List<double[]>
                {
                    new []{42.34,22.67,1234.6},
                    new []{42.35,22.68, 1237.9},
                    new []{42.36,22.69, 1300},
                    new []{42.67,23.21, 1301.2}
                }
            };

        public static RouteEditFormModel ValidTestRouteEditModel()
            => new()
            {
                Name = "Edited route name",
                StartLocationName = "Edited start location area",
                FinishLocationName = "Edited finish location area"
            };

    }
}
