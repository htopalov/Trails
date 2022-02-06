let drawnRoutePoints = [];
let drawnRouteTotalLength = 0;

map.on('draw:created',
    function (e) {
        editableLayers.eachLayer(function (layer) {
            editableLayers.removeLayer(layer);
        });

        let group = L.layerGroup();

        drawnRoutePoints = e.layer.getLatLngs().map(el => [el.lat, el.lng]);

        let route = new L.Polyline(drawnRoutePoints,
            {
                color: '#c700ac',
                weight: 4,
                smoothFactor: 1,
                fillOpacity: 0.5
            });

        let rawCoordinates = e.layer.getLatLngs();
        for (let i = 0; i < rawCoordinates.length - 1; i++) {
            drawnRouteTotalLength += rawCoordinates[i].distanceTo(rawCoordinates[i + 1]);
        }

        drawnRouteTotalLength /= 1000; //convert from meters to kilometers

        route.bindPopup(`Route for your event is ${drawnRouteTotalLength.toFixed(2)} kilometers long.`);

        group.addLayer(route);

        let startCircle = L.circle(drawnRoutePoints[0],
            100,
            {
                color: '#000000',
                fillColor: '#00d948',
                weight: 0.5,
                fillOpacity: 0.2
            }).bindPopup('Start');

        let finishCircle = L.circle(drawnRoutePoints[drawnRoutePoints.length - 1],
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
    });
