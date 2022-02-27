using System.Text;
using System.Xml.Serialization;
using Trails.Data.DomainModels;
using Trails.GPXProcessor.Models.Export;
using static Trails.GPXProcessor.ProcessorConstants;

namespace Trails.GPXProcessor
{
    public static class RouteProcessor
    {
        public static string Serialize(Route route)
        {
            var builder = new StringBuilder();
            var xmlRootAttribute = new XmlRootAttribute("gpx");
            xmlRootAttribute.Namespace = RootAttributeNamespace;
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("xsi", SchemaInstanceNamespace);
            serializerNamespaces.Add("schemaLocation", SchemaLocationNamespace);
            var xmlSerializer = new XmlSerializer(typeof(ExportGPXRouteModel), xmlRootAttribute);
            using var stringWriter = new Utf8StringWriter(builder);

            var routePointsList = new List<ExportRoutePointModel>();

            var points = route
                .RoutePoints
                .OrderBy(p => p.OrderNumber)
                .ToList();

            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];
                routePointsList.Add(new ExportRoutePointModel
                {
                    Latitude = point.Latitude.ToString(),
                    Longitude = point.Longitude.ToString(),
                    Altitude = point.Altitude.ToString()
                });
            }

            var routeMetadataModel = new ExportGPXMetadataModel()
            {
                Time = DateTime.UtcNow.ToString("u")
            };

            var exportTrackModel = new ExportRouteTrackModel()
            {
                Name = route.Name,
                RoutePoints = routePointsList
            };

            var routeModel = new ExportGPXRouteModel()
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
