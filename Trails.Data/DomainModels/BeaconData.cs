using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class BeaconData
    {
        public BeaconData() 
            => this.Id = Guid.NewGuid().ToString();

        [Key]
        [MaxLength(EntityIdMaxLength)]
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double Speed { get; set; }

        [MaxLength(BeaconImeiMaxLength)]
        public string BeaconImei { get; set; }

        [ForeignKey(nameof(Participant))]
        public string ParticipantId { get; set; }
        public Participant Participant { get; set; }

        [ForeignKey(nameof(Beacon))]
        public string BeaconId { get; set; }
        public Beacon Beacon { get; set; }
    }
}
