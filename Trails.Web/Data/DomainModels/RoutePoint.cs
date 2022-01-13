﻿using System.ComponentModel.DataAnnotations;

namespace Trails.Web.Data.DomainModels
{
    public class RoutePoint
    {
        public RoutePoint()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string RouteId { get; set; }
        public Route Route { get; set; }

    }
}