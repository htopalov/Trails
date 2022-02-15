namespace Trails.Models.Beacon
{
    public class AllBeaconsModel
    {
        public int BeaconsPerPage = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalBeacons { get; set; }

        public List<BaseBeaconModel> Beacons { get; set; }
    }
}
