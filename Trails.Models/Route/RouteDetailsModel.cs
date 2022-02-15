using Trails.Models.RoutePoint;

namespace Trails.Models.Route
{
    public class RouteDetailsModel : IRouteModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string StartLocationName { get; set; }

        public string FinishLocationName { get; set; }

        public double Length { get; set; }

        public double MinimumAltitude { get; set; }

        public double MaximumAltitude { get; set; }

        public string CreatorId { get; set; }

        public List<RoutePointExportModel> RoutePoints { get; set; }
    }
}
