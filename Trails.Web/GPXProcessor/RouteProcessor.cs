using System.Text;
using System.Xml.Serialization;
using Trails.Web.GPXProcessor.Models.Export;
using Route = Trails.Web.Data.DomainModels.Route;

namespace Trails.Web.GPXProcessor
{
    public static class RouteProcessor
    {


        public static string SerializeRoute(Route route)
        {
            var builder = new StringBuilder();
            var xmlRootAttribute = new XmlRootAttribute("gpx");
            xmlRootAttribute.Namespace = ProcessorConstants.RootAttributeNamespace;
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add("xsi", ProcessorConstants.SchemaInstanceNamespace);
            serializerNamespaces.Add("schemaLocation", ProcessorConstants.SchemaLocationNamespace);
            var xmlSerializer = new XmlSerializer(typeof(ExportGPXRouteModel),xmlRootAttribute);
            using var stringWriter = new StringWriter(builder);

            var routePointsList = route.RoutePoints.ToList();

            var routeMetadataModel = new ExportGPXMetadataModel()
            {
                Time = DateTime.UtcNow.ToString("u")
            };

            var exportTrackModel = new ExportRouteTrackModel()
            {
                Name = route.Name,
                RoutePoints = new ExportRoutePointModel[routePointsList.Count]
            };

            for (int i = 0; i < routePointsList.Count; i++)
            {
                var point = routePointsList[i];
                var exportRoutePointModel = new ExportRoutePointModel()
                {
                    Latitude = point.Latitude.ToString(),
                    Longitude = point.Longitude.ToString()
                };
                exportTrackModel.RoutePoints[i] = exportRoutePointModel;
            }

            var routeModel = new ExportGPXRouteModel()
            {
                Creator = ProcessorConstants.CreatorAttribute,
                Metadata = routeMetadataModel,
                Track = exportTrackModel
            };

            xmlSerializer.Serialize(stringWriter,routeModel,serializerNamespaces);
            return builder.ToString().Trim();
        }
    }
}
