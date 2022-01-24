using Trails.Web.Areas.Administration.Models.Beacon;

namespace Trails.Web.Areas.Administration.Services.Beacon
{
    public interface IBeaconService
    {
        Task<bool> CreateBeaconAsync(BeaconFormModel beaconFormModel);

        Task<AllBeaconsModel> GetAllBeaconsAsync(int currentPage = 1, int beaconsPerPage = int.MaxValue);

        Task<bool> DeleteBeaconAsync(string beaconId);
    }
}
