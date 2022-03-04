using Trails.Data.DomainModels;

namespace Trails.Api.Models
{
    public class BeaconDataBroadcastModel
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double Speed { get; set; }

        public string EventId { get; set; }

        public string ParticipantId { get; set; }
    }
}
