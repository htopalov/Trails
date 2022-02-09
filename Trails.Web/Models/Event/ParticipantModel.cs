using Trails.Web.Data.DomainModels;

namespace Trails.Web.Models.Event
{
    public class ParticipantModel
    {
        public string? UserId { get; set; }
        public User User { get; set; }

        public string? EventId { get; set; }
        public Data.DomainModels.Event Event { get; set; }

        public string? BeaconId { get; set; }
        public Beacon Beacon { get; set; }

        public bool IsApproved { get; set; }
    }
}
