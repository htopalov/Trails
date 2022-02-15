let drawnRoutePoints = [];
let drawnRouteTotalLength = 0;

map.on('draw:created',
    function (e) {
        editableLayers.eachLayer(function (layer) {
            editableLayers.removeLayer(layer);
        });

        isUploadButtonClicked = false;
        drawnRoutePoints.length = 0;
        drawnRouteTotalLength = 0;

        document.getElementById("line-chart").style.display = 'none';

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

        setupRouteIcons(drawnRoutePoints[0], drawnRoutePoints[drawnRoutePoints.length - 1], route, group);
    });
