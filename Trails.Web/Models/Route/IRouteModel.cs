namespace Trails.Web.Models.Route
{
    public interface IRouteModel
    {
        public string Name { get; set; }

        public string StartLocationName { get; set; }

        public string FinishLocationName { get; set; }
    }
}
