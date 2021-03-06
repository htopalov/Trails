using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class Route
    {
        public Route()
        {
            this.Id = Guid.NewGuid().ToString();
            this.RoutePoints = new List<RoutePoint>();
        }

        [Key]
        [MaxLength(EntityIdMaxLength)]
        public string Id { get; set; }

        [Required]
        [MaxLength(RouteNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string StartLocationName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FinishLocationName { get; set; }

        public double Length { get; set; }

        public double MinimumAltitude { get; set; }

        public double MaximumAltitude { get; set; }

        [ForeignKey(nameof(Event))]
        public string EventId { get; set; }
        public Event Event { get; set; }

        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public List<RoutePoint> RoutePoints { get; set; }
    }
}
