function setupParticipantsExistingPaths(path, pathsGroup, markersGroup) {
    pathsGroup.eachLayer(function (layer) {
        if (path.participantId === layer.options.id) {
            for (let i = 0; i < path.currentParticipantPathPoints.length; i++) {
                let point = path.currentParticipantPathPoints[i];
                layer.addLatLng(L.latLng(point[0], point[1], point[2]));
            }
        }
    });
    markersGroup.eachLayer(function (layer) {
        if (path.participantId === layer.options.id) {
            layer.setLatLng(path.currentParticipantPathPoints[path.currentParticipantPathPoints.length - 1]);
        }
    });
}