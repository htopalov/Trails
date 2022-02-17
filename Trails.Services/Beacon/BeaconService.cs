using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Models.Beacon;
using Trails.Security;

namespace Trails.Services.Beacon
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

        public async Task<BeaconFormModel> GetBeaconToEditByIdAsync(string id)
        {
            var beacon = await this.dbContext
                .Beacons
                .FirstOrDefaultAsync(b => b.Id == id);

            return this.mapper.Map<BeaconFormModel>(beacon);
        }

        public async Task<AllBeaconsModel> GetAllBeaconsAsync(
            int currentPage = 1,
            int beaconsPerPage = int.MaxValue)
        {
            var allBeacons = await this.dbContext
                .Beacons
                .ToListAsync();

            var totalBeacons = allBeacons.Count;

            var pagedBeacons = allBeacons
                .Skip((currentPage - 1) * beaconsPerPage)
                .Take(beaconsPerPage)
                .ToList();

            var mappedBeacons = this.mapper
                .Map<List<BaseBeaconModel>>(pagedBeacons);

            return new AllBeaconsModel
            {
                TotalBeacons = totalBeacons,
                CurrentPage = currentPage,
                Beacons = mappedBeacons,
            };

        }

        public async Task<bool> CreateBeaconAsync(BeaconFormModel beaconFormModel)
        {
            var isExisting = await dbContext
                .Beacons
                .AnyAsync(b => b.Imei == beaconFormModel.Imei);

            if (isExisting)
            {
                return false;
            }

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

        public async Task<bool> EditBeaconAsync(string id, BeaconFormModel beaconFormModel)
        {
            var beacon = await this.dbContext
                .Beacons
                .FindAsync(id);

            if (beacon == null)
            {
                return false;
            }

            this.mapper.Map(beaconFormModel, beacon);

            this.dbContext
                .Beacons
                .Update(beacon);

            var updated = await this.dbContext
                .SaveChangesAsync();

            return updated > 0;
        }


        public async Task<bool> DeleteBeaconAsync(string beaconId)
        {
            var beacon = await this.dbContext
                .Beacons
                .FirstOrDefaultAsync(b => b.Id == beaconId);

            if (beacon == null)
            {
                return false;
            }

            this.dbContext
                .Beacons
                .Remove(beacon);

             var deleted = await this.dbContext
                 .SaveChangesAsync();

             return deleted > 0;
        }
    }
}
