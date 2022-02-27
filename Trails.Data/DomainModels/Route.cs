using System.ComponentModel.DataAnnotations;
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

        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public List<RoutePoint> RoutePoints { get; set; }
    }
}
