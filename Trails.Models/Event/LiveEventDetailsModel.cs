using Trails.Models.Participant;
using Trails.Models.Route;
using Trails.Models.RoutePoint;

namespace Trails.Models.Event
{
    public class LiveEventDetailsModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<RoutePointExportModel> RoutePoints { get; set; }

        public List<LiveParticipantDetailsModel> Participants { get; set; }

    }
}
