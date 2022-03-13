using System.Text;
using System.Xml.Serialization;
using Trails.GPXProcessor.Models.Export;
using static Trails.GPXProcessor.ProcessorConstants;

namespace Trails.GPXProcessor
{
    public static class RouteProcessor
    {
        public static string Serialize(List<ExportPointModel> points)
        {
            var builder = new StringBuilder();
            var xmlRootAttribute = new XmlRootAttribute("gpx");
            xmlRootAttribute.Namespace = RootAttributeNamespace;
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("xsi", SchemaInstanceNamespace);
            serializerNamespaces.Add("schemaLocation", SchemaLocationNamespace);
            var xmlSerializer = new XmlSerializer(typeof(ExportGPXRouteModel), xmlRootAttribute);
            using var stringWriter = new Utf8StringWriter(builder);

            var routeMetadataModel = new ExportGPXMetadataModel
            {
                Time = DateTime.UtcNow.ToString("u")
            };

            var exportTrackModel = new ExportRouteTrackModel
            {
                Name = RouteName,
                RoutePoints = points
            };

            var routeModel = new ExportGPXRouteModel
            {
                Creator = CreatorAttribute,
                Metadata = routeMetadataModel,
                Track = exportTrackModel
            };

            xmlSerializer.Serialize(stringWriter, routeModel, serializerNamespaces);
            return builder.ToString().Trim();
        }
    }
}
