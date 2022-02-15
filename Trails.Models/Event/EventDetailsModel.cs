using Trails.Data.DomainModels;
using Trails.Data.Enums;

namespace Trails.Models.Event
{
    public class EventDetailsModel : IEventModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public EventType Type { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public double Length { get; set; }

        public bool IsApproved { get; set; }

        public string RouteId { get; set; }

        public string Image { get; set; }

        public string CreatorId { get; set; }

        public List<Participant> Participants { get; set; }
    }   
}
