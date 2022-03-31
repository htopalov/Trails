using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Trails.Data.DomainModels;
using Trails.Data.Enums;
using Trails.Models.Event;

namespace Trails.Test.EventServiceTests
{
    public static class EventServiceTestData
    {
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
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    Name = "Test event 3",
                    Description = "Description for event",
                    Length = 4.3,
                    StartDate = DateTime.UtcNow.AddDays(9),
                    EndDate = DateTime.UtcNow.AddDays(12),
                    Type = EventType.Orienteering,
                    DifficultyLevel = DifficultyLevel.VeryEasy,
                    IsApproved = true,
                    IsDeleted = true,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001",
                    Participants = new List<Participant>
                    {
                        new ()
                        {
                            Id = "00000000-0000-0000-0000-000000000999",
                            IsApproved = false,
                            EventId = "00000000-0000-0000-0000-000000000003"
                        }
                    }
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    Name = "Test event 4",
                    Description = "Description for event",
                    Length = 12,
                    StartDate = DateTime.UtcNow.AddDays(-10),
                    EndDate = DateTime.UtcNow.AddDays(-9),
                    Type = EventType.Orienteering,
                    DifficultyLevel = DifficultyLevel.VeryEasy,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "10000000-0000-0000-0000-000000000001",
                    ImageId = "10000000-0000-0000-0000-000000000001"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000005",
                    Name = "Test event 5",
                    Description = "Description for event",
                    Length = 21,
                    StartDate = DateTime.UtcNow.AddHours(-1),
                    EndDate = DateTime.UtcNow.AddDays(1),
                    Type = EventType.Other,
                    DifficultyLevel = DifficultyLevel.Moderate,
                    IsApproved = true,
                    IsDeleted = false,
                    IsModifiedByCreator = false,
                    CreatorId = "10000000-0000-0000-0000-000000000001",
                    RouteId = "00000000-3333-4444-0000-000000000000",
                    Route = ValidTestRoute(),
                    ImageId = "10000000-0000-0000-0000-000000000001"
                }
            };

        public static List<Participant> GetTestParticipants() 
            => new()
            {
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000111",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000001",
                    UserId = "10000000-0000-0000-0000-000000000001"
                },
                new ()
                {
                    Id = "00000000-0000-0000-0000-000000000222",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000002",
                    UserId = "20000000-0000-0000-0000-000000000002"
                },
                new ()
                {
                    Id = "00000000-0000-0000-0000-000000000333",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000003",
                    UserId = "30000000-0000-0000-0000-000000000003"
                },
                new ()
                {
                    Id = "00000000-0000-0000-0000-000000000444",
                    IsApproved = false,
                    UserId = "40000000-0000-0000-0000-000000000004"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000555",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000004",
                    UserId = "50000000-0000-0000-0000-000000000005"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000666",
                    IsApproved = true,
                    EventId = "00000000-0000-0000-0000-000000000005",
                    UserId = "60000000-0000-0000-0000-000000000006",
                    BeaconId = "00000000-0000-0220-2200-000000000002",
                    Beacon = ValidTestBeacon(),
                    BeaconData = new List<BeaconData>
                    {
                        new()
                        {
                            Id = "aaaaaaaa-0000-0000-0000-000000000001",
                            Timestamp = DateTime.UtcNow.AddMinutes(-3),
                            Latitude = 42.78,
                            Longitude = 24.45,
                            Altitude = 345,
                            Speed = 3.43,
                            BeaconImei = "000000000000002",
                            ParticipantId = "00000000-0000-0000-0000-000000000666",
                        },
                        new()
                        {
                            Id = "aaaaaaaa-0000-0000-0000-000000000002",
                            Timestamp = DateTime.UtcNow.AddMinutes(-2),
                            Latitude = 42.72,
                            Longitude = 23.45,
                            Altitude = 380,
                            Speed = 2.43,
                            BeaconImei = "000000000000002",
                            ParticipantId = "00000000-0000-0000-0000-000000000666",
                        }
                    }
                }
            };

        public static Beacon ValidTestBeacon()
            => new()
            {
                Id = "00000000-0000-0220-2200-000000000002",
                Imei = "000000000000002",
                Description = "Description for this beacon",
                SimCardNumber = "+359887123789",
                KeyHash = "ghjcLXgACzNr3FNoDOMsYAM5Oa5zPcU8"
            };

        public static List<User> GetTestUsers()
            => new()
            {
                new ()
                {
                    Id = "10000000-0000-0000-0000-000000000001",
                    FirstName = "Georgi",
                    LastName = "Petrov",
                    UserName = "gpetrov",
                    Email = "gpetrov@test.com",
                    PhoneNumber = "0883567899",
                    CountryName = "Bulgaria"
                },
                new ()
                {
                    Id = "20000000-0000-0000-0000-000000000002",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    UserName = "iIvanov",
                    Email = "iIvanov@test.com",
                    PhoneNumber = "+359886456345",
                    CountryName = "Bulgaria"
                },
                new()
                {
                    Id = "30000000-0000-0000-0000-000000000003",
                    FirstName = "Nikolay",
                    LastName = "Stoyanov",
                    UserName = "nstoyanov",
                    Email = "nstoyanov@test.com",
                    PhoneNumber = "0883753209",
                    CountryName = "Bulgaria"
                },
                new()
                {
                    Id = "40000000-0000-0000-0000-000000000004",
                    FirstName = "Martin",
                    LastName = "Kirilov",
                    UserName = "mkirilov",
                    Email = "mkirilov@test.com",
                    PhoneNumber = "0884678001",
                    CountryName = "Bulgaria"
                },
                new()
                {
                    Id = "50000000-0000-0000-0000-000000000005",
                    FirstName = "Vasil",
                    LastName = "Ivanov",
                    UserName = "vivanov",
                    Email = "vivanov@test.com",
                    PhoneNumber = "+359884670113",
                    CountryName = "Bulgaria"
                },
                new()
                {
                    Id = "60000000-0000-0000-0000-000000000006",
                    FirstName = "Daniel",
                    LastName = "Vulchev",
                    UserName = "dvulchev",
                    Email = "dvulchev@test.com",
                    PhoneNumber = "+359885000591",
                    CountryName = "Bulgaria"
                }
            };

        public static EventFormModel ValidEventFormModel()
            => new()
            {
                Name = "Valid event",
                Description = "Description for valid event",
                StartDate = DateTime.UtcNow.AddDays(5),
                EndDate = DateTime.UtcNow.AddDays(6),
                Length = 12.5,
                DifficultyLevel = 1,
                Type = 2,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                Image = new FormFile(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes("this is image string representation")),
                    0,
                    1024,
                    "Image",
                    "dummyImage.png")
            };

        public static EventFormModel ExistingEventFormModel()
            => new()
            {
                Name = "Test event 5",
                Description = "Description for valid event",
                StartDate = DateTime.UtcNow.AddDays(5),
                EndDate = DateTime.UtcNow.AddDays(6),
                Length = 12.5,
                DifficultyLevel = 1,
                Type = 2,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                Image = new FormFile(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes("this is image string representation")),
                    0,
                    1024,
                    "Image",
                    "dummyImage.png")
            };

        public static EventEditFormModel ValidEventEditFormModel()
            => new()
            {
                Name = "edited",
                Description = "Description for valid event",
                StartDate = DateTime.UtcNow.AddDays(5),
                EndDate = DateTime.UtcNow.AddDays(6),
                Length = 12.5,
                DifficultyLevel = DifficultyLevel.VeryEasy,
                Type = EventType.Orienteering
            };

        public static EventImageEditModel ValidTestImage()
            => new()
            {
                Image = new FormFile(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes("this is image string representation")),
                    0,
                    1024,
                    "Image",
                    "dummyImage.png")
            };

        public static Route ValidTestRoute()
            => new()
            {
                Id = "00000000-3333-4444-0000-000000000000",
                Name = "Valid test route",
                StartLocationName = "Start line",
                FinishLocationName = "Finish line",
                Length = 20,
                MinimumAltitude = 333,
                MaximumAltitude = 1234.5,
                CreatorId = "10000000-0000-0000-0000-000000000001",
                EventId = "00000000-0000-0000-0000-000000000005",
                RoutePoints = new List<RoutePoint>
                {
                    new ()
                    {
                        Id = "11111111-0000-0000-0000-000000000000",
                        OrderNumber = 1,
                        Latitude = 42.25,
                        Longitude = 25.67,
                        Altitude = 456,
                        RouteId = "00000000-3333-4444-0000-000000000000"
                    },
                    new ()
                    {
                        Id = "22222222-0000-0000-0000-000000000000",
                        OrderNumber = 2,
                        Latitude = 42.26,
                        Longitude = 25.68,
                        Altitude = 457,
                        RouteId = "00000000-3333-4444-0000-000000000000"
                    },
                    new ()
                    {
                        Id = "33333333-0000-0000-0000-000000000000",
                        OrderNumber = 3,
                        Latitude = 42.27,
                        Longitude = 25.69,
                        Altitude = 458,
                        RouteId = "00000000-3333-4444-0000-000000000000"
                    }
                }
            };
    }
}
