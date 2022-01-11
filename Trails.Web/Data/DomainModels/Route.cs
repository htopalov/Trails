using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Trails.Web.Data.DataConstants.Common;

namespace Trails.Web.Data.DomainModels
{
    public class Route
    {
        public Route()
        {
            this.Id = Guid.NewGuid().ToString();
            this.GeoPoints = new List<GeoPoint>();
            this.Checkpoints = new List<GeoPoint>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string StartLocationName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FinishLocationName { get; set; }

        [ForeignKey(nameof(Event))]
        public string EventId { get; set; }
        public Event Event { get; set; }

        public IEnumerable<GeoPoint> GeoPoints { get; set; }

        public IEnumerable<GeoPoint> Checkpoints { get; set; }
    }
}
