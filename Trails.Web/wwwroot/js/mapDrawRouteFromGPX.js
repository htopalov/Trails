let importedRoutePoints = [];
let importedRouteTotalLength = 0;

function drawImportedRoute(routePoints) {
    editableLayers.eachLayer(function (layer) {
        editableLayers.removeLayer(layer);
    });

    let group = L.layerGroup();

    let pointList = [];
    for (let i = 0; i < routePoints.length; i++) {
        let currentPoint = routePoints[i];
        pointList.push(new L.LatLng(currentPoint[0], currentPoint[1]));
    }

    let route = new L.Polyline(pointList,
        {
            color: '#c700ac',
            weight: 4,
            smoothFactor: 1,
            fillOpacity: 0.5
        });

    importedRoutePoints = route.getLatLngs().map(el => [el.lat, el.lng]);

    let rawCoordinates = route.getLatLngs();
    for (let i = 0; i < rawCoordinates.length - 1; i++) {
        importedRouteTotalLength += rawCoordinates[i].distanceTo(rawCoordinates[i + 1]);
    }

    importedRouteTotalLength /= 1000;

    route.bindPopup(`Route for your event is ${importedRouteTotalLength.toFixed(2)} kilometers long.`);

    group.addLayer(route);

    setupRouteIcons(pointList[0], pointList[pointList.length - 1], route,group);
}
