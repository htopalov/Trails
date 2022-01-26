using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.Data.DomainModels
{
    public class Route
    {
        public Route()
        {
            this.Id = Guid.NewGuid().ToString();
            this.RoutePoints = new List<double[]>();
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

        public double Length { get; set; }

        public ICollection<double[]> RoutePoints { get; set; }


        //TODO: OPTIONALLY ADD ABILITY TO UPLOAD GPX PREDEFINED ROUTES TO SYSTEM AND LOAD THEM TO MAP.... IMPLEMENT GPX PARSER
    }
}
