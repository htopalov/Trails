﻿using System.Xml.Serialization;

namespace Trails.Web.GPXProcessor.Models.Export
{
    [XmlType("trkpt")]
    public class ExportRoutePointModel
    {
        [XmlAttribute("lat")]
        public string Latitude { get; set; }

        [XmlAttribute("lon")]
        public string Longitude { get; set; }
    }
}