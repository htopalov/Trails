using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class Participant
    {
        public Participant()
            => this.Id = Guid.NewGuid().ToString();

        [Key]
        [MaxLength(EntityIdMaxLength)]
        public string Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Event))]
        public string EventId { get; set; }
        public Event Event { get; set; }

        [ForeignKey(nameof(Beacon))]
        public string BeaconId { get; set; }
        public Beacon Beacon { get; set; }

        public bool IsApproved { get; set; }

        public List<BeaconData> BeaconData { get; set; }
    }
}
