using Trails.Models.Route;

namespace Trails.Services.Route
{
    public interface IRouteService
    {
        Task<bool> CreateRouteAsync(RouteCreateModel routeCreateModel, string currentUserId);

        Task<RouteDetailsModel> GetRouteAsync(string routeId);

        Task<RouteEditFormModel> GetRouteToEditAsync(string routeId);

        Task<bool> EditRouteAsync(string routeId, RouteEditFormModel routeEditFormModel);
    }
}
