namespace Trails.Web.BeaconDataApi.Models
{
    public class BeaconDataDtoPost
    {
        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double Speed { get; set; }

        public string BeaconId { get; set; }
    }
}
