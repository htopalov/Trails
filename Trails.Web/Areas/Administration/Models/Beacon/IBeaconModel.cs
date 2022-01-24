namespace Trails.Web.Areas.Administration.Models.Beacon
{
    public interface IBeaconModel
    {
        public string Imei { get; set; }

        public string SimCardNumber { get; set; }

        public string Description { get; set; }
    }
}
