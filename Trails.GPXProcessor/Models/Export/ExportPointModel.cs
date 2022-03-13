using System.Xml.Serialization;

namespace Trails.GPXProcessor.Models.Export
{
    [XmlType("trkpt")]
    public class ExportPointModel
    {
        [XmlAttribute("lat")]
        public string Latitude { get; set; }

        [XmlAttribute("lon")]
        public string Longitude { get; set; }

        [XmlElement("ele")]
        public string Altitude { get; set; }

    }
}
