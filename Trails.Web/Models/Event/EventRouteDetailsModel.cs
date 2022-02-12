using Trails.Web.Models.Route;

namespace Trails.Web.Models.Event
{
    public class EventRouteDetailsModel
    {
        public string Id { get; set; }
        public string StartLocationName { get; set; }

        public string FinishLocationName { get; set; }

        public double MinimumAltitude { get; set; }

        public double MaximumAltitude { get; set; }

        public List<EventRoutePointsDetailsModel> RoutePoints { get; set; }
    }
}
