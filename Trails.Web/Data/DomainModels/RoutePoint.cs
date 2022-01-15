using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trails.Web.Data.DomainModels
{
    public class RoutePoint
    {
        public RoutePoint() =>
            this.Id = Guid
                    .NewGuid()
                    .ToString();

        [Key]
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        //route need to be created first and after that add points to it in order to work
        [ForeignKey(nameof(Route))]
        public string RouteId { get; set; }
        public Route Route { get; set; }

    }
}
