using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Web.BeaconDataApi.Models;
using Trails.Web.Data;

namespace Trails.Web.BeaconDataApi.Services.BeaconDataService
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

        //public async Task<bool> CreateBeaconDataAsync(BeaconDataDtoPost beaconDataDto)
        //{
        //    var isBeacon = await this.dbContext
        //        .Beacons
        //        .AnyAsync(b => b.Imei == beaconDataDto.BeaconImei);

        //    if (!isBeacon)
        //    {
        //        return false;
        //    }

        //    //search for which race/user is this data
        //    //and check for event start and end date
        //    //if timestamp is not between those two return false

        //    //may need to add more fields on Beacon domain model for this logic to happen
        //}
    }
}