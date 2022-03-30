using System;
using System.Collections.Generic;
using Trails.Api.Models;
using Trails.Data.DomainModels;
using Trails.Data.Enums;

namespace Trails.Test.BeaconDataServiceTests
{
    public static class BeaconDataTestData
    {
        public static List<Event> GetTestEvents() 
            => new()
            {
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Name = "Passed event",
                    Description = "Description for event",
                    Length = 14.3,
                    StartDate = DateTime.UtcNow.AddDays(-5),
                    EndDate = DateTime.UtcNow.AddDays(-4),
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
                    Name = "Live event",
                    Description = "Description for event",
                    Length = 18,
                    StartDate = DateTime.UtcNow.AddHours(-12),
                    EndDate = DateTime.UtcNow.AddDays(1),
                    Type = EventType.Running,
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
                    Id = "00000000-0000-0000-0000-000000000003",
                    Name = "Future event",
                    Description = "Description for event",
                    Length = 18,
                    StartDate = DateTime.UtcNow.AddDays(20),
                    EndDate = DateTime.UtcNow.AddDays(21),
                    Type = EventType.Running,
                    DifficultyLevel = DifficultyLevel.Demanding,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                }
            };

        public static List<Participant> GetTestParticipants() 
            => new()
            {
                new()
                {
                    Id = "00000000-0000-0000-0000-111111111111",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000001",
                    BeaconId = "00000000-2200-0000-0000-000000000022"
                },
                new ()
                {
                    Id = "00000000-0000-0000-0000-222222222222",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000001"
                },
                new ()
                {
                    Id = "00000000-0000-0000-0000-444444444444",
                    IsApproved = false,
                    EventId = "00000000-0000-0000-0000-000000000002",
                    BeaconId = "00000000-4000-0000-0000-000000000004"
                },
                new ()
                {
                    Id = "00000000-0000-0000-0000-555555555555",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000003",
                    BeaconId = "00000000-3300-0000-0000-000000000033"
                },
            };

        public static List<Beacon> GetTestBeacons() 
            => new()
            {
                new ()
                {
                    Id = "00000000-1000-0000-0000-000000000001",
                    Description = "Description for beacon",
                    Imei = "000000000000001",
                    SimCardNumber = "0883456789",
                    KeyHash = "23345fsdgsfd4445gsg"
                },
                new ()
                {
                    Id = "00000000-2200-0000-0000-000000000022",
                    Description = "Description for beacon",
                    Imei = "000000000000002",
                    SimCardNumber = "0883456711",
                    KeyHash = "23345fsdgggggg5d4445gsg"
                },
                new()
                {
                    Id = "00000000-3300-0000-0000-000000000033",
                    Description = "Description for beacon",
                    Imei = "000000000000003",
                    SimCardNumber = "0883456722",
                    KeyHash = "23345fsdgggggg5d4445AsA"
                },
                new()
                {
                    Id = "00000000-4000-0000-0000-000000000004",
                    Description = "Description for beacon",
                    Imei = "000000000000004",
                    SimCardNumber = "0883456666",
                    KeyHash = "23345fsbmsfd4445gsg"
                }
            };

        public static BeaconDataDtoPost DataWithIncorrectBeaconImei()
            => new()
            {
                Latitude = 42.6778,
                Longitude = 25.4890,
                Altitude = 560.4,
                Speed = 3.56,
                BeaconImei = "123456789123456"
            };

        public static BeaconDataDtoPost DataWithCorrectBeaconImei()
            => new()
            {
                Latitude = 43.6998,
                Longitude = 24.5850,
                Altitude = 1260.9,
                Speed = 1.56,
                BeaconImei = "000000000000004"
            };

        public static BeaconDataDtoPost DataWithCorrectBeaconImeiForFutureEvent()
            => new()
            {
                Latitude = 43.6998,
                Longitude = 24.5850,
                Altitude = 1260.9,
                Speed = 1.56,
                BeaconImei = "000000000000003"
            };

        public static BeaconDataDtoPost DataWithCorrectBeaconImeiForPastEvent()
            => new()
            {
                Latitude = 43.6778,
                Longitude = 24.6050,
                Altitude = 260.9,
                Speed = 5.56,
                BeaconImei = "000000000000003"
            };
    }
}
