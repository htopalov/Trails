using System;
using System.Collections.Generic;
using Trails.Data.DomainModels;
using Trails.Data.Enums;

namespace Trails.Test.StatisticsServiceTests
{
    public static class StatisticsServiceTestData
    {
        public static List<User> GetTestUsers() 
            => new()
            {
                new()
                {
                    Id = "222793ae-5c88-4a38-b1d0-bcd396443507",
                    FirstName = "Ivan",
                    LastName = "Petkov",
                    BirthDate = DateTime.UtcNow.AddYears(-20),
                    CountryName = "Bulgaria",
                    PhoneNumber = "0884567901",
                    Gender = Gender.Male,
                    Email = "ivan@test.com",
                    UserName = "ivan_petkov"
                },
                new()
                {
                    Id = "222793ae-5c88-4a38-b1d0-bcd396447890",
                    FirstName = "Kalina",
                    LastName = "Balkanska",
                    BirthDate = DateTime.UtcNow.AddYears(-8),
                    CountryName = "Bulgaria",
                    PhoneNumber = "0885567123",
                    Gender = Gender.Female,
                    Email = "kalinka@test.com",
                    UserName = "kalina_kal"
                },
                new()
                {
                    Id = "676893ae-5c88-4a38-b1d0-ffd996447890",
                    FirstName = "Stanislav",
                    LastName = "Yordanov",
                    BirthDate = DateTime.UtcNow.AddYears(-30),
                    CountryName = "Bulgaria",
                    PhoneNumber = "0884237123",
                    Gender = Gender.Male,
                    Email = "stan@test.com",
                    UserName = "stanislavi"
                }
            };

        public static List<Participant> GetTestParticipants() 
            => new()
            {
                new()
                {
                    Id = "111793ae-5c88-4a38-b1d0-bmd396443909",
                    UserId = "221793ae-5c88-4a38-b1d0-bcd390443507",
                    EventId = "222793ae-5c88-4a38-b1d0-bcd396443444",
                    IsApproved = true
                },
                new()
                {
                    Id = "333793ae-5c88-4a38-b1d0-bhd396443000",
                    UserId = "022793ae-5c88-4a38-b4d0-bcd396447890",
                    EventId = "222793ae-5c88-4a38-b1d0-bcd396443444",
                    IsApproved = false
                }
            };

        public static List<Event> GetTestEvents()
            => new()
            {
                new()
                {
                    Id = "f4405704-4e58-4877-82eb-0f329548d118",
                    Name = "First test event",
                    Description = "Description for first test event",
                    StartDate = DateTime.UtcNow.AddDays(3),
                    EndDate = DateTime.UtcNow.AddDays(4),
                    Type = EventType.Cycling,
                    DifficultyLevel = DifficultyLevel.Moderate,
                    Length = 12.3,
                    IsApproved = true,
                    IsModifiedByCreator = false,
                    IsDeleted = false,
                    RouteId = "222793ae-5c88-4a38-b1d0-bcd396412346",
                    ImageId = "222793ae-5c88-4a38-b1d0-fgd396447882"
                },
                new()
                {
                    Id = "f4405704-4e58-4877-82eb-0f329548d128",
                    Name = "Second test event",
                    Description = "Description for second test event",
                    StartDate = DateTime.UtcNow.AddDays(15),
                    EndDate = DateTime.UtcNow.AddDays(17),
                    Type = EventType.Running,
                    DifficultyLevel = DifficultyLevel.Easy,
                    Length = 42.3,
                    IsApproved = false,
                    IsModifiedByCreator = false,
                    IsDeleted = false,
                    RouteId = "222793ae-5c88-4a38-b1d0-bcd396412345",
                    ImageId = "222793ae-5c88-4a38-b1d0-fgd396447882"
                },
                new()
                {
                    Id = "f4405704-4e58-4877-82eb-0f329548d119",
                    Name = "Third test event",
                    Description = "Description for third test event",
                    StartDate = DateTime.UtcNow.AddDays(10),
                    EndDate = DateTime.UtcNow.AddDays(11),
                    Type = EventType.SpeedHiking,
                    DifficultyLevel = DifficultyLevel.Easy,
                    Length = 2.3,
                    IsApproved = true,
                    IsModifiedByCreator = false,
                    IsDeleted = false,
                    RouteId = "222793ae-5c88-4a38-b1d0-bcd396412345",
                    ImageId = "222793ae-5c88-4a38-b1d0-fgd396447882"
                },
                new()
                {
                    Id = "f4405704-4e58-4877-82eb-0f329548d120",
                    Name = "Fourth test event",
                    Description = "Description for fourth test event",
                    StartDate = DateTime.UtcNow.AddDays(30),
                    EndDate = DateTime.UtcNow.AddDays(31),
                    Type = EventType.Orienteering,
                    DifficultyLevel = DifficultyLevel.Extreme,
                    Length = 102.3,
                    IsApproved = true,
                    IsModifiedByCreator = false,
                    IsDeleted = true,
                    RouteId = "222793ae-5c88-4a38-b1d0-bcd396412345",
                    ImageId = "222793ae-5c88-4a38-b1d0-fgd396447882"
                },
                new()
                {
                    Id = "f4405704-4e58-4877-82eb-0f329548d121",
                    Name = "Fifth test event",
                    Description = "Description for fifth test event",
                    StartDate = DateTime.UtcNow.AddDays(5),
                    EndDate = DateTime.UtcNow.AddDays(6),
                    Type = EventType.Other,
                    DifficultyLevel = DifficultyLevel.VeryEasy,
                    Length = 1.3,
                    IsApproved = true,
                    IsModifiedByCreator = false,
                    IsDeleted = true,
                    RouteId = "222793ae-5c88-4a38-b1d0-bcd396412345",
                    ImageId = "222793ae-5c88-4a38-b1d0-fgd396447882"
                }
            };

        public static List<Route> GetTestRoutes()
            => new()
            {
                new Route
                {
                    Id = "222793ae-5c88-4a38-b1d0-bcd396412345",
                    Name = "Test route for fifth event",
                    StartLocationName = "Start point",
                    FinishLocationName = "Finish line",
                    Length = 1.3,
                    MinimumAltitude = 320.1,
                    MaximumAltitude = 345.6,
                    CreatorId = "222793ae-5c88-4a38-b1d0-bcd396443507",
                    EventId = "f4405704-4e58-4877-82eb-0f329548d121",
                    RoutePoints = new List<RoutePoint>()
                }
            };
    }
}
