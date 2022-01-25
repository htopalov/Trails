using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.BeaconDataApi.Models
{
    public class BeaconDataDtoPost
    {
        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double Speed { get; set; }

        [RegularExpression(
            ValidationConstants.ImeiPattern,
            ErrorMessage = ErrorMessages.InvalidImeiFormatError)]
        public string BeaconImei { get; set; }
    }
}
