using AutoMapper;
using Trails.Web.Areas.Administration.Models.Beacon;
using Trails.Web.Data;
using Trails.Web.Infrastructure;

namespace Trails.Web.Areas.Administration.Services.Beacon
{
    public class BeaconService : IBeaconService
    {
        private readonly TrailsDbContext dbContext;
        private readonly IMapper mapper;

        public BeaconService(TrailsDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> CreateBeaconAsync(BeaconFormModel beaconFormModel)
        {
            beaconFormModel.Key = SecurityProvider
                .KeyHasher(beaconFormModel.Key);


            var beacon = this.mapper
                .Map<Data.DomainModels.Beacon>(beaconFormModel);

            await this.dbContext
                .Beacons
                .AddAsync(beacon);

            var created = await dbContext
                .SaveChangesAsync();

            return created > 0;
        }
    }
}
