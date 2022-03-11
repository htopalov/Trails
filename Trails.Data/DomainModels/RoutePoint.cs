using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class RoutePoint
    {
        public RoutePoint() 
            => this.Id = Guid.NewGuid().ToString();

        [Key]
        [MaxLength(EntityIdMaxLength)]
        public string Id { get; set; }

        public int OrderNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        [ForeignKey(nameof(Route))]
        public string RouteId { get; set; }
        public Route Route { get; set; }

    }
}