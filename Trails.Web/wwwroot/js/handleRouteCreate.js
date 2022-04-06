function handleRouteCreateFunc(XsrfToken,creatorId) {
    document.getElementById('submitButton').addEventListener('click',
        async (e) => {
            e.preventDefault();
            let form = document.getElementById('routeForm');
            let formData = new FormData(form);
            let name = formData.get('Name');
            let startLocationName = formData.get('StartLocation');
            let finishLocationName = formData.get('FinishLocation');
            let queryParams = new Proxy(new URLSearchParams(window.location.search),
                {
                    get: (searchParams, prop) => searchParams.get(prop)
                });
            let eventId = queryParams.forEventId;
            if (eventId === null) {
                alert('Event does not exist.');
                return;
            }
            showSpinner();
            await fetch('/Route/Create',
                {
                    method: 'post',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': `${XsrfToken}`
                    },
                    body: JSON.stringify({
                        creatorId,
                        eventId,
                        name,
                        startLocationName,
                        finishLocationName,
                        routePoints: isUploadButtonClicked
                            ? importedRoutePoints
                            : drawnRoutePoints,
                        length: isUploadButtonClicked
                            ? importedRouteTotalLength
                            : drawnRouteTotalLength,
                        minimumAltitude: isUploadButtonClicked
                            ? minAltitude
                            : 0,
                        maximumAltitude: isUploadButtonClicked
                            ? maxAltitude
                            : 0
                    })
                })
                .then((response) => {
                    hideSpinner();
                    if (response.ok) {
                        window.location = `/event/events`;
                    } else if (response.status === 400) {
                        window.location.reload();
                    }
                })
                .catch(() => {
                    hideSpinner();
                    window.location.reload();
                });
            
        });

    let gpxBtnUpload = L.easyButton({
        states: [
            {
                icon: 'fas fa-file-upload',
                title: 'Plot GPX File',
                onClick: () => {
                    let fileSelector = document.getElementById('gpxFileUploadElement');
                    fileSelector.value = '';
                    fileSelector.click();
                    fileSelector.addEventListener('change',
                        async (e) => {
                            e.stopImmediatePropagation();
                            let file = e.target.files[0];
                            isUploadButtonClicked = true;
                            drawImportedRoute(await processGPXAsync(file));
                        });
                }
            }
        ]
    });
    gpxBtnUpload.addTo(map);
}