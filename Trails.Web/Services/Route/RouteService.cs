using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Web.Data;
using Trails.Web.Data.DomainModels;
using Trails.Web.Models.Route;

namespace Trails.Web.Services.Route
{
    public class RouteService : IRouteService
    {
        private readonly TrailsDbContext dbContext;
        private readonly IMapper mapper;

        public RouteService(TrailsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> CreateRouteAsync(RouteCreateModel routeCreateModel)
        {
            var eventForRoute = await this.dbContext
                .Events
                .FindAsync(routeCreateModel.EventId);

            if (eventForRoute == null)
            {
                return false;
            }

            var isRouteExisting = await this.dbContext
                .Routes
                .AnyAsync(r => r.Name == routeCreateModel.Name);

            if (isRouteExisting)
            {
                return false;
            }

            var route = this.mapper
                .Map<Data.DomainModels.Route>(routeCreateModel);

            eventForRoute.Route = route;

            await this.dbContext.SaveChangesAsync();

            for (int i = 0; i < routeCreateModel.RoutePoints.Count; i++)
            {
                var lat = routeCreateModel.RoutePoints[i][0];
                var lng = routeCreateModel.RoutePoints[i][1];
                var point = new RoutePoint
                {
                    Latitude = lat,
                    Longitude = lng,
                    Route = route
                };
                route.RoutePoints.Add(point);
            }

            var created = await this.dbContext
                .SaveChangesAsync();

            return created > 0;
        }
    }
}
