using System.ComponentModel.DataAnnotations;

namespace Trails.Models.Participant
{
    public class ParticipantBeaconModel
    {
        [Required]
        public string BeaconId { get; set; }

        [Required]
        public string ParticipantId { get; set; }
    }
}
