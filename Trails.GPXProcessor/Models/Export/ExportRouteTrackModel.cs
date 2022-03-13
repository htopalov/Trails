using System.Xml.Serialization;

namespace Trails.GPXProcessor.Models.Export
{
    [XmlType("trk")]
    public class ExportRouteTrackModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("trkseg")]
        public List<ExportPointModel> RoutePoints { get; set; }
    }
}
