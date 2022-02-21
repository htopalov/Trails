namespace Trails.Models.Event
{
    public class AllUnapprovedEventsModel : IListEventModel
    {
        public int EventsPerPage = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalEvents { get; set; }

        public List<UnapprovedEventModel> Events { get; set; }
    }
}
