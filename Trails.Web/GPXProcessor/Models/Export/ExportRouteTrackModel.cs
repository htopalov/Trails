using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trails.Web.Common;

namespace Trails.Web.GPXProcessor.Models.Export
{
    [XmlType("trk")]
    public class ExportRouteTrackModel
    {
        [XmlElement("name")]
        [Required]
        [StringLength(
            ValidationConstants.RouteNameMaxLength,
            ErrorMessage = ErrorMessages.StringLengthError,
            MinimumLength = ValidationConstants.RouteNameMinLength)]
        public string Name { get; set; }

        [XmlArray("trkseg")]
        public List<ExportRoutePointModel> RoutePoints { get; set; }
    }
}
