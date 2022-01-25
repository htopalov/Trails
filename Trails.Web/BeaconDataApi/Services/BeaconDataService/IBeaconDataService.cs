using Trails.Web.BeaconDataApi.Models;

namespace Trails.Web.BeaconDataApi.Services.BeaconDataService
{
    public interface IBeaconDataService
    {
        Task<bool> CreateBeaconDataAsync(BeaconDataDtoPost beaconDataDto);
    }
}
