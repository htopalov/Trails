using System.ComponentModel.DataAnnotations;

namespace Trails.Web.Data.DomainModels
{
    public class BeaconData
    {
        public BeaconData() => 
            this.Id = Guid
                .NewGuid()
                .ToString();

        [Key]
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double Speed { get; set; }

        public string BeaconImei { get; set; }

        public string BeaconId { get; set; }
        public Beacon Beacon { get; set; }
    }
}
