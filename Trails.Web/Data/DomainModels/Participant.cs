namespace Trails.Web.Data.DomainModels
{
    public class Participant
    {
        public Participant()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string? UserId { get; set; }
        public User User { get; set; }

        public string? EventId { get; set; }
        public Event Event { get; set; }

        public string? TeamName { get; set; }

        public string? BeaconId { get; set; }
        public Beacon Beacon { get; set; }
    }
}
