namespace Trails.Models.Event
{
    public interface IEventModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Length { get; set; }
    }
}
