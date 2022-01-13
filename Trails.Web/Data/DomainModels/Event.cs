using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trails.Web.Data.Enums;

using static Trails.Web.Data.DataConstants.Common;
using static Trails.Web.Data.DataConstants.Event;

namespace Trails.Web.Data.DomainModels
{
    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Teams = new List<Team>();
            this.UsersEvents = new List<UserEvent>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public EventType Type { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public double Length { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public bool IsTeamEvent { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Route))]
        public string RouteId { get; set; }
        public Route Route { get; set; }

        public ICollection<UserEvent> UsersEvents { get; set; }

        public ICollection<Team> Teams { get; set; }

        //TODO:ADD PHOTO TO EVENT SAVED TO LOCAL FILE SYSTEM

        //TODO: OPTIONALLY ADD ABILITY TO UPLOAD GPX PREDEFINED ROUTES TO SYSTEM AND LOAD THEM TO MAP....
    }
}
