function drawImportedRoute(routePoints) {
    let pointList = [];
    for (var i = 0; i < routePoints.length; i++) {
        let currentPoint = routePoints[i];
        pointList.push(new L.LatLng(currentPoint[0], currentPoint[1]));
    }

    let route = new L.Polyline(pointList, {
        color: '#c700ac',
        weight: 4,
        smoothFactor: 1,
        fillOpacity: 0.5
    });
    route.addTo(map);
}
