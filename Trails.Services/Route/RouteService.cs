﻿using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Data.DomainModels;
using Trails.GPXProcessor;
using Trails.Models.Route;

namespace Trails.Services.Route
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
            var hasAltitude = routeCreateModel.MaximumAltitude != 0;

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

            route.CreatorId = eventForRoute.CreatorId;

            eventForRoute.Route = route;

            await this.dbContext.SaveChangesAsync();

            for (int i = 0; i < routeCreateModel.RoutePoints.Count; i++)
            {
                var lat = routeCreateModel.RoutePoints[i][0];
                var lng = routeCreateModel.RoutePoints[i][1];
                var alt = hasAltitude 
                    ? routeCreateModel.RoutePoints[i][2] 
                    : 0;
                var point = new RoutePoint
                {
                    OrderNumber = i,
                    Latitude = lat,
                    Longitude = lng,
                    Altitude = alt,
                    Route = route
                };
                route.RoutePoints.Add(point);
            }

            var created = await this.dbContext
                .SaveChangesAsync();

            return created > 0;
        }

        public async Task<RouteDetailsModel> GetRouteAsync(string routeId)
        {
            var route = await this.dbContext
                .Routes
                .Include(r=>r.RoutePoints)
                .FirstOrDefaultAsync(r=>r.Id == routeId);
            
            if (route == null)
            {
                return null;
            }

            route.RoutePoints = route
                .RoutePoints
                .OrderBy(p => p.OrderNumber)
                .ToList();

            var routeDetailsModel = this.mapper
                .Map<RouteDetailsModel>(route);

            return routeDetailsModel;
        }

        public async Task<RouteEditFormModel> GetRouteToEditAsync(string routeId)
        {
            var route = await this.dbContext
                .Routes
                .FindAsync(routeId);

            if (route == null)
            {
                return null;
            }

            var routeToEdit = this.mapper.
                Map<RouteEditFormModel>(route);

            return routeToEdit;
        }

        public async Task<bool> EditRouteAsync(string routeId, RouteEditFormModel routeEditFormModel)
        {
            var route = await this.dbContext
                .Routes
                .FindAsync(routeId);

            if (route == null)
            {
                return false;
            }

            this.mapper
                .Map(routeEditFormModel, route);

            this.dbContext
                .Routes
                .Update(route);

            var updated = await this.dbContext
                .SaveChangesAsync();

            return updated > 0;
        }

        public async Task<byte[]> GenerateGPXAsync(string routeId)
        {
            var route = await this.dbContext
                .Routes
                .Include(r=>r.RoutePoints)
                .FirstOrDefaultAsync(r=>r.Id == routeId);

            if (route == null)
            {
                return null;
            }

            var gpxXml = RouteProcessor.Serialize(route);

            await using var memoryStream = new MemoryStream();

            var fileBytes = Encoding.Default.GetBytes(gpxXml);

            await memoryStream.WriteAsync(fileBytes, 0, fileBytes.Length);

            return fileBytes;
        }
    }
}