using Trails.Web.Areas.Administration.Models.Beacon;

namespace Trails.Web.Areas.Administration.Services.Beacon
{
    public interface IBeaconService
    {
        Task<bool> CreateBeaconAsync(BeaconFormModel beaconFormModel);
    }
}
