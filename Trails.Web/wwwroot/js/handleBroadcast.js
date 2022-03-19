function handleBroadcast(participantPaths, routePoints, startPoint,participantsCount, eventId) {
    let pathsGroup = L.layerGroup();
    let markersGroup = L.layerGroup();
    let pathColorElements = document.getElementsByClassName('random');
    let checkBoxes = document.querySelectorAll('input[type="checkbox"]');

    let sidebar = L.control.sidebar({
        autopan: true,
        closeButton: true,
        container: 'sidebar',
        position: 'left'
    }).addTo(map);

    drawImportedRoute(routePoints);

    for (let i = 0; i < participantsCount; i++) {
        setupSidebarAndMapElements(startPoint, pathColorElements[i], pathsGroup, markersGroup, checkBoxes[i]);
    }

    editableLayers.addLayer(pathsGroup);
    editableLayers.addLayer(markersGroup);

    pathsGroup.eachLayer(function (layer) {
        layer.addLatLng(L.latLng(startPoint[0], startPoint[1]));
    });

    for (let i = 0; i < participantPaths.length; i++) {
        if (participantPaths[i].currentParticipantPathPoints.length > 0) {
            setupParticipantsExistingPaths(participantPaths[i], pathsGroup, markersGroup);
        }
    }

    startConnection();

    connection.onclose(async () => {
        await startConnection();
    });

    connection.on("BroadcastData",
        (data) => {
            if (data['eventId'] === eventId) {
                onBroadcast(data, pathsGroup, markersGroup);
            }
        });
}