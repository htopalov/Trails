using Trails.Api.Models;

namespace Trails.Models.Participant
{
    public class LiveParticipantDetailsModel
    {
        public string Id { get; set; }

        public string Fullname { get; set; }

        public string CountryName { get; set; }

        public string Gender { get; set; }

        public List<BeaconDataBroadcastModel> BeaconData { get; set; }
    }
}
