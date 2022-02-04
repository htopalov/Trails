using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Trails.Web.GPXProcessor.Models.Export
{
    [XmlType("metadata")]
    public class ExportGPXMetadataModel
    {
        [Required]
        [XmlElement("time")]
        public string Time { get; set; }
    }
}
