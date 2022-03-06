namespace Trails.Models.Route
{
    public class AllRoutesModel
    {
        public int RoutesPerPage = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalRoutes { get; set; }

        public string SearchRoute { get; set; }

        public List<BaseRouteModel> Routes { get; set; }
    }
}
