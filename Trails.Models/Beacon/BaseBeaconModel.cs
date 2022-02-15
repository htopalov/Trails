namespace Trails.Models.Beacon
{
    public class BaseBeaconModel : IBeaconModel
    {
        public string Id { get; set; }

        public string Imei { get; set; }

        public string SimCardNumber { get; set; }

        public string Description { get; set; }

        //public bool IsAssignedToParticipant { get; set; }
    }
}
