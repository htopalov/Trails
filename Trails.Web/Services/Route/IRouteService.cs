using Trails.Web.Models.Route;

namespace Trails.Web.Services.Route
{
    public interface IRouteService
    {
        Task<bool> CreateRouteAsync(RouteCreateModel routeCreateModel);
    }
}
