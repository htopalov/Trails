using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Api.Models;
using Trails.Data;
using Trails.Data.DomainModels;

namespace Trails.Api.Services
{
    public class BeaconDataService : IBeaconDataService
    {
        private readonly TrailsDbContext dbContext;
        private readonly IMapper mapper;

        public BeaconDataService(TrailsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<BeaconDataBroadcastModel> CreateBeaconDataAsync(BeaconDataDtoPost beaconDataDto)
        {
            beaconDataDto.Timestamp = DateTime.UtcNow;

            var participant = await this.dbContext
                    .Participants
                    .Include(p=>p.Beacon)
                    .Include(p=>p.Event)
                    .SingleOrDefaultAsync(p => p.Beacon.Imei == beaconDataDto.BeaconImei);

            if (participant == null)
            {
                return null;
            }

            var isInCorrectTime = beaconDataDto.Timestamp >= participant.Event.StartDate
                                  && beaconDataDto.Timestamp <= participant.Event.EndDate;

            if (!isInCorrectTime)
            {
                return null;
            }

            beaconDataDto.BeaconId = participant.Beacon.Id;
            beaconDataDto.ParticipantId = participant.Id;

            var mappedData = this.mapper
                .Map<BeaconData>(beaconDataDto);

           await this.dbContext
                .BeaconData
                .AddAsync(mappedData);

           var result = await this.dbContext
               .SaveChangesAsync();

           if (result > 0)
           {
               return new BeaconDataBroadcastModel
               {
                   Latitude = beaconDataDto.Latitude,
                   Longitude = beaconDataDto.Longitude,
                   Altitude = beaconDataDto.Altitude,
                   Speed = beaconDataDto.Speed,
                   EventId = participant.EventId,
                   ParticipantId = participant.Id,
                   Timestamp = beaconDataDto.Timestamp
               };
           }

           return null;
        }
    }
}