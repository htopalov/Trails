using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;
using Trails.Web.Data.Enums;


namespace Trails.Web.Data.DomainModels
{
    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Participants = new List<Participant>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public EventType Type { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public double Length { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; } //?? or just set to null everywhere ??

        public string? RouteId { get; set; }
        public Route Route { get; set; }

        public string? ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}
