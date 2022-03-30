using System.Collections.Generic;
using Trails.Data.DomainModels;
using Trails.Models.Beacon;

namespace Trails.Test.BeaconServiceTests
{
    public static class BeaconServiceTestData
    {
        public static List<Beacon> GetTestBeacons() 
            => new()
            {
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Imei = "000000000000001",
                    Description = "Description for this beacon",
                    SimCardNumber = "+359887123456",
                    KeyHash = "DBXcLXgACzNr3FNoDOMsYAM5Oa5zPcU8"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    Imei = "000000000000002",
                    Description = "Description for this beacon",
                    SimCardNumber = "+359887123789",
                    KeyHash = "ghjcLXgACzNr3FNoDOMsYAM5Oa5zPcU8"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    Imei = "000000000000003",
                    Description = "Description for this beacon",
                    SimCardNumber = "+359887123004",
                    KeyHash = "ghjcLXgACzNr3FNo567snmM5Oa5zPcU3"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    Imei = "000000000000004",
                    Description = "Description for this beacon",
                    SimCardNumber = "+359887123005",
                    KeyHash = "ghjcLXgACzNr3FNo888snmM5Oa5zPcU3"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000005",
                    Imei = "000000000000005",
                    Description = "Description for this beacon",
                    SimCardNumber = "+359887123014",
                    KeyHash = "ghjcLXgACGkr3FNo567snmM5Oa5zPcU3"
                },
                new()
                {
                    Id = "00000000-0000-0000-0000-000000000006",
                    Imei = "000000000000006",
                    Description = "Description for this beacon",
                    SimCardNumber = "+359887123025",
                    KeyHash = "ghjcLXgACzNr3FN0i88snmM5Oa5zPcU3"
                }
            };

        public static BeaconFormModel CorrectBeaconCreateTest()
            => new()
            {
                Imei = "111111111111119",
                Description = "Description for single beacon",
                SimCardNumber = "0883123456",
                Key = "1234567890"
            };

        public static BeaconFormModel IncorrectBeaconCreateTest()
            => new()
            {
                Imei = "000000000000006",
                Description = "Description for single beacon",
                SimCardNumber = "0883123476",
                Key = "1234567890"
            };

        public static List<Participant> GetTestParticipants()
            => new()
            {
                new Participant
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    IsApproved = true,
                    BeaconId = "00000000-0000-0000-0000-000000000006"
                }
            };
    }
}
