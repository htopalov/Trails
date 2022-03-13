function setupSidebarAndMapElements(startPoint,pathColorElement,pathsGroup,markersGroup,checkBox) {
    let rndColor = generateRandomColor();
    let icon = pathColorElement;
    icon.style.color = rndColor;
    let id = icon.id;
    let marker = L.marker([startPoint[0], startPoint[1]], { id });
    let path = new L.Polyline(new L.LatLng(startPoint[0], startPoint[1]),
        {
            id,
            color: rndColor,
            weight: 2,
            smoothFactor: 1,
            fillOpacity: 0.5
        });
    pathsGroup.addLayer(path);
    markersGroup.addLayer(marker);

    checkBox.addEventListener('change', (e) => {
        let checkboxId = e.target.id;
        if (e.target.checked) {
            //show
            pathsGroup.eachLayer(function (layer) {
                if (checkboxId === layer.options.id) {
                    layer._path.style.display = 'block';
                }
            });
            markersGroup.eachLayer(function (layer) {
                if (checkboxId === layer.options.id) {
                    layer._icon.style.display = 'block';
                    layer._shadow.style.display = 'block';
                }
            });
        } else {
            //hide
            pathsGroup.eachLayer(function (layer) {
                if (checkboxId === layer.options.id) {
                    layer._path.style.display = 'none';
                }
            });
            markersGroup.eachLayer(function (layer) {
                if (checkboxId === layer.options.id) {
                    layer._icon.style.display = 'none';
                    layer._shadow.style.display = 'none';
                }
            });
        }
    });
}