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

    let startCircle = L.circle(routePoints[0],
        100,
        {
            color: '#000000',
            fillColor: '#00d948',
            weight: 0.5,
            fillOpacity: 0.2
        }).bindPopup('Start');

    let finishCircle = L.circle(routePoints[routePoints.length - 1],
        100,
        {
            color: '#000000',
            fillColor: '#e8000f',
            weight: 0.5,
            fillOpacity: 0.2
        }).bindPopup('Finish');

    group.addLayer(startCircle);
    group.addLayer(finishCircle);
    editableLayers.addLayer(group);
    map.fitBounds(route.getBounds());
}
