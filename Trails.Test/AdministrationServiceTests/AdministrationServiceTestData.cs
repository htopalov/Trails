using System;
using System.Collections.Generic;
using Trails.Data.DomainModels;
using Trails.Data.Enums;
using Trails.Models.Participant;
using Event = Trails.Data.DomainModels.Event;

namespace Trails.Test.AdministrationServiceTests
{
    public static class AdministrationServiceTestData
    {
        public static List<Participant> GetTestParticipants()
            => new()
            {
                new Participant
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000009"
                },
                new Participant
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000009"
                },
                new Participant
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    IsApproved = true,
                    BeaconId = "00000000-1000-0000-0000-000000000001"
                },
                new Participant
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    IsApproved = false
                }
            };

        public static List<Beacon> GetTestBeacons()
            => new()
            {
                new Beacon
                {
                    Id = "00000000-1000-0000-0000-000000000001",
                    Description = "Description for beacon",
                    Imei = "000000000000001",
                    SimCardNumber = "0883456789",
                    KeyHash = "23345fsdgsfd4445gsg"
                },
                new Beacon
                {
                    Id = "00000000-2200-0000-0000-000000000022",
                    Description = "Description for beacon",
                    Imei = "000000000000002",
                    SimCardNumber = "0883456711",
                    KeyHash = "23345fsdgggggg5d4445gsg"
                },
                new Beacon
                {
                    Id = "00000000-3300-0000-0000-000000000033",
                    Description = "Description for beacon",
                    Imei = "000000000000003",
                    SimCardNumber = "0883886711",
                    KeyHash = "23345fsvvvg5d4445gsg"
                }
            };

        public static List<Event> GetTestEvents()
            => new()
            {
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Name = "Cycling event",
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
                    Name = "Running event",
                    Description = "Description for event",
                    Length = 18,
                    StartDate = DateTime.UtcNow.AddDays(10),
                    EndDate = DateTime.UtcNow.AddDays(11),
                    Type = EventType.Running,
                    DifficultyLevel = DifficultyLevel.Demanding,
                    IsApproved = false,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    Name = "Hiking event",
                    Description = "Description for event",
                    Length = 38,
                    StartDate = DateTime.UtcNow.AddDays(20),
                    EndDate = DateTime.UtcNow.AddDays(22),
                    Type = EventType.SpeedHiking,
                    DifficultyLevel = DifficultyLevel.Demanding,
                    IsApproved = true,
                    IsDeleted = true,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    Name = "Event without route",
                    Description = "Description for event",
                    Length = 38,
                    StartDate = DateTime.UtcNow.AddDays(4),
                    EndDate = DateTime.UtcNow.AddDays(5),
                    Type = EventType.Orienteering,
                    DifficultyLevel = DifficultyLevel.Moderate,
                    IsApproved = false,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000009",
                    Name = "Event to prepare",
                    Description = "Description for event",
                    Length = 38,
                    StartDate = DateTime.UtcNow.AddDays(3),
                    EndDate = DateTime.UtcNow.AddDays(4),
                    Type = EventType.Orienteering,
                    DifficultyLevel = DifficultyLevel.Moderate,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000008999",
                    Name = "Passed event",
                    Description = "Description for event",
                    Length = 38,
                    StartDate = DateTime.UtcNow.AddDays(-15),
                    EndDate = DateTime.UtcNow.AddDays(-14),
                    Type = EventType.Orienteering,
                    DifficultyLevel = DifficultyLevel.Moderate,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                }
            };

        public static List<Route> GetTestRoutes()
            => new()
            {
                new Route
                {
                    Id = "10000000-0000-0110-0000-000000000001",
                    Name = "First Test Route",
                    StartLocationName = "Start line",
                    FinishLocationName = "Finish line",
                    Length = 13.6,
                    MinimumAltitude = 320,
                    MaximumAltitude = 1235.7,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    EventId = "00000000-0000-0000-0000-000000000003"
                },
                new Route
                {
                    Id = "20000000-0000-0220-0000-000000000002",
                    Name = "Second Test Route",
                    StartLocationName = "Start line",
                    FinishLocationName = "Finish line",
                    Length = 27.6,
                    MinimumAltitude = 122.4,
                    MaximumAltitude = 450.7,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    EventId = "00000000-0000-0000-0000-000000000002"
                }
            };

        public static User GetTestUser()
            => new()
            {
                Id = "10000000-0000-0000-0000-000000000001",
                FirstName = "Ivan",
                LastName = "Georgiev",
                Gender = Gender.Male,
                BirthDate = DateTime.UtcNow.AddYears(-12),
                Email = "ivan@abv.bg",
                UserName = "vankata",
                CountryName = "Bulgaria",
                PhoneNumber = "0885456908"
            };

        public static ParticipantBeaconModel GetTestParticipantBeaconModelWithInvalidParticipant()
            => new()
            {
                BeaconId = "00000000-2200-0000-0000-000000000022",
                ParticipantId = "00000000-9900-0000-9999-000000000022"
            };

        public static ParticipantBeaconModel GetTestParticipantBeaconModelWithInvalidBeacon()
            => new()
            {
                BeaconId = "00000000-1000-0000-0000-000000000011",
                ParticipantId = "00000000-0000-0000-0000-000000000003"
            };

        public static ParticipantBeaconModel GetTestParticipantBeaconModelWithValidBeacon()
            => new()
            {
                BeaconId = "00000000-1000-0000-0000-000000000001",
                ParticipantId = "00000000-0000-0000-0000-000000000003"
            };
    }
}
