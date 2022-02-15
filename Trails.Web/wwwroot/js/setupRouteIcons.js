function setupRouteIcons(startPoint,finishPoint,route,group) {
    let startIcon = L.icon({
        iconUrl: '/images/map/startIcon.svg',
        iconSize: [32, 37],
        iconAnchor: [1, 37],
        popupAnchor: [5, -28]
    });

    let startMarker = L.marker(startPoint, { icon: startIcon }).bindPopup('Start Location');

    let finishIcon = L.icon({
        iconUrl: '/images/map/finishIcon.svg',
        iconSize: [32, 37],
        iconAnchor: [1, 37],
        popupAnchor: [5, -28]
    });

    let finishMarker = L.marker(finishPoint, { icon: finishIcon }).bindPopup('Finish Location');

    group.addLayer(startMarker);
    group.addLayer(finishMarker);
    editableLayers.addLayer(group);
    map.fitBounds(route.getBounds());
}