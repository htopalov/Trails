async function processGPXAsync(file) {
    let processedRoutePoints = [];
    if (!file.name.endsWith('.gpx')) {
        alert('Only gpx file type is allowed.');
        return;
    }
    showSpinner();
    let contents = await readFile(file);
    let pointsAsText = contents.match(/((<trkpt)|(<rtept)) \w+=\"[0-9]+\.[0-9]+\" \w+=\"[0-9]+\.[0-9]+\"/gm);
    let altitudeAsText = contents.match(/<ele>[0-9]+.[0-9]+<\/ele>/gm);
    if (pointsAsText === null || pointsAsText.length === 0) {
        alert('File does not contain any coordinates.');
        hideSpinner();
        return;
    }
    for (let i = 0; i < pointsAsText.length; i++) {
        let pointText = pointsAsText[i];
        let latlng = pointText.match(/([\d.]+)/g);
        let altitude = altitudeAsText[i].match(/([\d.]+)/g);
        let point = [];
        let lat = parseFloat(latlng[0]);
        let lng = parseFloat(latlng[1]);
        let alt = parseFloat(altitude);
        point.push(lat);
        point.push(lng);
        point.push(alt);
        processedRoutePoints.push(point);
    }
    hideSpinner();
    return processedRoutePoints;
}



function readFile(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = res => {
            resolve(res.target.result);
        };
        reader.onerror = err => reject(err);

        reader.readAsText(file);
    });
}