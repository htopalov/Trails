using System.Xml.Serialization;

namespace Trails.Web.GPXProcessor.Models.Export
{
    [XmlType("metadata")]
    public class ExportGPXMetadataModel
    {
        [XmlElement("time")]
        public string Time { get; set; }
    }
}
