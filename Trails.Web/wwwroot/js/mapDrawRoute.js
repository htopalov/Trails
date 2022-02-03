let layers = [];
let routePoints = [];

map.on('draw:created',
    function (e) {
        if (layers.length === 1) {
            alert('One event can have only one route.');
            map.removeLayer(e.layer);
        }

        layers.push(e.layer);
        editableLayers.addLayer(e.layer);
        routePoints = e.layer.getLatLngs().map(el => [el.lat, el.lng]);
        e.layer.bindPopup('Route for your event');
        e.layer

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
    });