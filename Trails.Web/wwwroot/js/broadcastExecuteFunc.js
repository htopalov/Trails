function onBroadcast(data, pathsGroup, markersGroup) {
    pathsGroup.eachLayer(function (layer) {
        if (data.participantId === layer.options.id) {
            layer.addLatLng(L.latLng(data.latitude, data.longitude, data.altitude));
        }
    });
    markersGroup.eachLayer(function (layer) {
        if (data.participantId === layer.options.id) {
            layer.setLatLng([data.latitude, data.longitude]);
        }
    });

    let participantDivToUpdate = document.getElementById(`${data.participantId}-current-location`);
    participantDivToUpdate.children[2].textContent = `Lat: ${data.latitude.toFixed(4)}`;
    participantDivToUpdate.children[4].textContent = `Lng: ${data.longitude.toFixed(4)}`;
    participantDivToUpdate.children[6].textContent = `Alt: ${data.altitude.toFixed(2)} m`;
    participantDivToUpdate.children[8].textContent = `Speed: ${data.speed.toFixed(2)} km/h`;
}