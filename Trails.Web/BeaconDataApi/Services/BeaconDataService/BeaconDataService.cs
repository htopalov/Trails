using AutoMapper;
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
    }
}