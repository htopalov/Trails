using System.ComponentModel.DataAnnotations;
using Trails.Data.DomainModels;
using static Trails.Common.ValidationConstants;
using static Trails.Common.ErrorMessages;

namespace Trails.Api.Models
{
    public class BeaconDataDtoPost
    {
        public DateTime Timestamp { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Range(MinAltitude, MaxAltitude)]
        public double Altitude { get; set; }

        [Range(MinSpeed,MaxSpeed)]
        public double Speed { get; set; }

        [RegularExpression(
            ImeiPattern,
            ErrorMessage = InvalidImeiFormatError)]
        public string BeaconImei { get; set; }

        public string ParticipantId { get; set; }

        public string BeaconId { get; set; }
    }
}
