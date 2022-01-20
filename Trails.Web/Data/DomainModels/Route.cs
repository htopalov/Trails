using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.Data.DomainModels
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
        [MaxLength(ValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.NameMaxLength)]
        public string StartLocationName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.NameMaxLength)]
        public string FinishLocationName { get; set; }

        public ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
