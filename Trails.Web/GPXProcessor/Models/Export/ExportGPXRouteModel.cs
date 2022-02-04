using System.Xml.Serialization;

namespace Trails.Web.GPXProcessor.Models.Export
{
    public class ExportGPXRouteModel
    {
        [XmlAttribute("creator")]
        public string Creator { get; set; }

        [XmlElement("metadata")]
        public ExportGPXMetadataModel Metadata { get; set; }

        [XmlElement("trk")]
        public ExportRouteTrackModel Track { get; set; }
    }
}
