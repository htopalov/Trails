namespace Trails.Models.Event
{
    public interface IListEventModel
    {
        public int CurrentPage { get; set; }

        public int TotalEvents { get; set; }
    }
}
