namespace Trails.Models.Event
{
    public class BaseEventModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
