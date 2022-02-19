namespace Trails.Models.Event
{
    public class ListEventsModel
    {
        public int EventsPerPage = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalEvents{ get; set; }

        public bool AreMine { get; set; } = false;

        public List<BaseEventModel> Events { get; set; }
    }
}
