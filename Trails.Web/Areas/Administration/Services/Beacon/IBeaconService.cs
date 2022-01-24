using Trails.Web.Areas.Administration.Models.Beacon;

namespace Trails.Web.Areas.Administration.Services.Beacon
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
