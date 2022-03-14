using Trails.Models.Route;

namespace Trails.Services.Route
{
    public interface IRouteService
    {
        Task<bool> CreateRouteAsync(RouteCreateModel routeCreateModel);

        Task<T> GetRouteAsync<T>(string routeId) where T : IRouteModel;

        Task<bool> EditRouteAsync(string routeId, RouteEditFormModel routeEditFormModel);

        Task<AllRoutesModel> GetAllRoutesAsync(
            string searchRoute = null,
            int currentPage = 1,
            int routesPerPage = int.MaxValue);

        Task<byte[]> GenerateGPXAsync(string routeId);
    }
}
