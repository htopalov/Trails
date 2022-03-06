namespace Trails.Models.Event
{
    public class ListEventsModel : IListEventModel
    {
        public int EventsPerPage = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalEvents{ get; set; }

        public string SearchEvent { get; set; }

        public string UserId { get; set; }

        public List<BaseEventModel> Events { get; set; }
    }
}
