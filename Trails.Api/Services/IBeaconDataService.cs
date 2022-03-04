using Trails.Api.Models;

namespace Trails.Api.Services
{
    public interface IBeaconDataService
    {
        Task<BeaconDataBroadcastModel> CreateBeaconDataAsync(BeaconDataDtoPost beaconDataDto);
    }
}
