let layers = [];
let routePoints = [];
let routeTotalLength = 0;

map.on('draw:created',
    function (e) {
        if (layers.length === 1) {
            alert('One event can have only one route.');
            map.removeLayer(e.layer);
        } else {
            layers.push(e.layer);
            editableLayers.addLayer(e.layer);
            routePoints = e.layer.getLatLngs().map(el => [el.lat, el.lng]);

            let rawCoordinates = e.layer.getLatLngs();
            for (let i = 0; i < rawCoordinates.length - 1; i++) {
                routeTotalLength += rawCoordinates[i].distanceTo(rawCoordinates[i + 1]);
            }

            routeTotalLength = routeTotalLength / 1000; //convert from meters to kilometers

            e.layer.bindPopup(`Route for your event is ${routeTotalLength.toFixed(2)} kilometers long.`);

            L.circle(routePoints[0],
                100,
                {
                    color: '#000000',
                    fillColor: '#00d948',
                    weight: 0.5,
                    fillOpacity: 0.2
                }).addTo(map).bindPopup('Start');

            L.circle(routePoints[routePoints.length - 1],
                100,
                {
                    color: '#000000',
                    fillColor: '#e8000f',
                    weight: 0.5,
                    fillOpacity: 0.2
                }).addTo(map).bindPopup('Finish');
        }
    });