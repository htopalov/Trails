using Trails.Models.Beacon;

namespace Trails.Services.Beacon
{
    public interface IBeaconService
    {
        Task<BeaconFormModel> GetBeaconToEditByIdAsync(string id);

        Task<AllBeaconsModel> GetAllBeaconsAsync(int currentPage = 1, int beaconsPerPage = int.MaxValue);

        Task<bool> CreateBeaconAsync(BeaconFormModel beaconFormModel);

        Task<bool> EditBeaconAsync(string id, BeaconFormModel beaconEditFormModel);

        Task<bool> DeleteBeaconAsync(string beaconId);
    }
}
