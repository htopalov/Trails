﻿using System.Xml.Serialization;

namespace Trails.Web.GPXProcessor.Models.Export
{
    [XmlType("trk")]
    public class ExportRouteTrackModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("trkseg")]
        public List<ExportRoutePointModel> RoutePoints { get; set; }
    }
}
